using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class NavigationPath : INavigationPath
    {
        private readonly NavigationPathImpl _navigationPathImpl;

        public NavigationPath(INavigation navigation)
        {
            _navigationPathImpl = new NavigationPathImpl(navigation);
        }

        public NavigationPath Add(string path)
        {
            _navigationPathImpl.Add(path);
            return this;
        }

        public NavigationPath Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public NavigationPath Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public NavigationPathResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationPathResult<TResult>(this);
        }

        public NavigationPathResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationPathResult<TResult>(this);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null)
        {
            return _navigationPathImpl.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }

    public class NavigationPath<TRootViewModel> : INavigationPath<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly NavigationPathImpl _navigationPathImpl;

        public NavigationPath(INavigation navigation)
        {
            _navigationPathImpl = new NavigationPathImpl(navigation);
        }

        public NavigationPath<TRootViewModel> Add(string path)
        {
            _navigationPathImpl.Add(path);
            return this;
        }

        public NavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public NavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public NavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationPathResult<TRootViewModel, TResult>(this);
        }

        public NavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationPathResult<TRootViewModel, TResult>(this);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null)
        {
            return _navigationPathImpl.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }

    internal class NavigationPathImpl
    {
        private INavigation _rootNavigation;
        private INavigation _lastNavigation;

        public NavigationPathImpl(INavigation navigation)
        {
            _rootNavigation = navigation;
            _lastNavigation = navigation;
        }

        public void Add(string path)
        {
            var navigation = new Navigation(path);
            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;
        }

        public void Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>(canNavigate);
            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;
        }

        public void Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter, canNavigate);
            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;
        }

        public void Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived, canNavigate);
            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;
        }

        public void Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            _lastNavigation.NextNavigation = navigation;
            _lastNavigation = navigation;
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null)
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
}
