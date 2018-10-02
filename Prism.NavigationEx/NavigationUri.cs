using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class NavigationUri : INavigationUri
    {
        private readonly NavigationUriImpl _navigationPathImpl;

        public NavigationUri(INavigation navigation)
        {
            _navigationPathImpl = new NavigationUriImpl(navigation);
        }

        public NavigationUri Add(string path)
        {
            _navigationPathImpl.Add(path);
            return this;
        }

        public NavigationUri Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public NavigationUri Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public NavigationUriResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationUriResult<TResult>(this);
        }

        public NavigationUriResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationUriResult<TResult>(this);
        }

        public (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationPathImpl.GetUriAndParameters(additionalParameters, additionalQueries);
        }
    }

    public class NavigationUri<TRootViewModel> : INavigationUri<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly NavigationUriImpl _navigationPathImpl;

        public NavigationUri(INavigation navigation)
        {
            _navigationPathImpl = new NavigationUriImpl(navigation);
        }

        public NavigationUri<TRootViewModel> Add(string path)
        {
            _navigationPathImpl.Add(path);
            return this;
        }

        public NavigationUri<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public NavigationUri<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public NavigationUriResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationPathImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationUriResult<TRootViewModel, TResult>(this);
        }

        public NavigationUriResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationPathImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationUriResult<TRootViewModel, TResult>(this);
        }

        public (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationPathImpl.GetUriAndParameters(additionalParameters, additionalQueries);
        }
    }

    internal class NavigationUriImpl
    {
        private INavigation _rootNavigation;
        private INavigation _lastNavigation;

        public NavigationUriImpl(INavigation navigation)
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

        public (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            var parameters = new NavigationParameters();
            if (additionalParameters != null)
            {
                foreach (var pair in additionalParameters)
                {
                    parameters.Add(pair.Key, pair.Value);
                }
            }

            var path = _rootNavigation.CreateNavigationUri(parameters, additionalQueries);

            return (path, parameters);
        }
    }
}
