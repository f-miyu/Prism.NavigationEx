using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public static class NavigationPath
    {
        public static INavigationPath<TViewModel> Create<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>
            {
                CanNavigate = canNavigate
            };
            return new NavigationPath<TViewModel>(navigation);
        }

        public static INavigationPath<TViewModel> Create<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>
            {
                Parameter = parameter,
                CanNavigate = canNavigate
            };
            return new NavigationPath<TViewModel>(navigation);
        }

        public static INavigationPathResult<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>
            {
                ResultReceived = resultReceived,
                CanNavigate = canNavigate
            };
            return new NavigationPathResult<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }

        public static INavigationPathResult<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>
            {
                Parameter = parameter,
                ResultReceived = resultReceived,
                CanNavigate = canNavigate
            };
            return new NavigationPathResult<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }
    }

    public class NavigationPath<TRootViewModel> : INavigationPath<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private INavigation _rootNavigation;
        private INavigation _lastNavigation;

        public NavigationPath(INavigation navigation)
        {
            _rootNavigation = navigation;
            _lastNavigation = navigation;
        }

        public INavigationPath<TRootViewModel> Add(string path)
        {
            var navigation = new Navigation
            {
                Path = path
            };

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return this;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>
            {
                CanNavigate = canNavigate
            };

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return this;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>
            {
                Parameter = parameter,
                CanNavigate = canNavigate
            };

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return this;
        }

        public INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>
            {
                ResultReceived = resultReceived,
                CanNavigate = canNavigate
            };

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return new NavigationPathResult<TRootViewModel, TResult>(this);
        }

        public INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>
            {
                Parameter = parameter,
                ResultReceived = resultReceived,
                CanNavigate = canNavigate
            };

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return new NavigationPathResult<TRootViewModel, TResult>(this);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null)
        {
            var parameters = new NavigationParameters();
            if (additionalParameters != null)
            {
                foreach (var pair in additionalParameters)
                {
                    parameters.Add(pair.Key, pair.Value);
                }
            }

            var path = _rootNavigation.CreateNavigationPath(parameters, additionalPathParameters);

            return (path, parameters);
        }
    }

    public class NavigationPathResult<TRootViewModel, TReceivedResult> : INavigationPathResult<TRootViewModel, TReceivedResult> where TRootViewModel : INavigationViewModel
    {
        private readonly INavigationPath<TRootViewModel> _navigationPath;

        public NavigationPathResult(INavigationPath<TRootViewModel> navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>(canNavigate);
        }

        public INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null)
        {
            return _navigationPath.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }
}
