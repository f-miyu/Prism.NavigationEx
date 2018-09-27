using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationPath
    {
        public static INavigationPath<TViewModel> Create<TViewModel>() where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>();
            return new NavigationPath<TViewModel>(navigation);
        }

        public static INavigationPath<TViewModel> Create<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter);
            return new NavigationPath<TViewModel>(navigation);
        }

        public static INavigationPath<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived); ;
            return new NavigationPath<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }

        public static INavigationPath<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived);
            return new NavigationPath<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }
    }

    internal class NavigationPath<TRootViewModel> : INavigationPath<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private INavigation _rootNavigation;
        private INavigation _lastNavigation;

        public NavigationPath(INavigation navigation)
        {
            _rootNavigation = navigation;
            _lastNavigation = navigation;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel>() where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>();

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return this;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter);

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return this;
        }

        public INavigationPath<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived);

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return new NavigationPath<TRootViewModel, TResult>(this);
        }

        public INavigationPath<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived);

            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;

            return new NavigationPath<TRootViewModel, TResult>(this);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null)
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

    internal class NavigationPath<TRootViewModel, TReceivedResult> : INavigationPath<TRootViewModel, TReceivedResult> where TRootViewModel : INavigationViewModel
    {
        private readonly INavigationPath<TRootViewModel> _navigationPath;

        public NavigationPath(INavigationPath<TRootViewModel> navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public INavigationPath<TRootViewModel> Add<TViewModel>() where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>();
        }

        public INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter);
        }

        public INavigationPath<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived);
        }

        public INavigationPath<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null)
        {
            return _navigationPath.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }
}
