using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class Navigation : INavigation
    {
        private readonly NavigationImpl _navigationImpl;

        public Navigation(INavigationPath navigationPath)
        {
            _navigationImpl = new NavigationImpl(navigationPath);
        }

        public Navigation Add(string path)
        {
            _navigationImpl.Add(path);
            return this;
        }

        public Navigation Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public Navigation Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public ResultNavigation<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new ResultNavigation<TResult>(this);
        }

        public ResultNavigation<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new ResultNavigation<TResult>(this);
        }

        public Navigation AddForTabbedPage(params ITab[] tabs)
        {
            _navigationImpl.AddForTabbedPage(tabs);
            return this;
        }

        public Navigation AddForTabbedPage(string path, params ITab[] tabs)
        {
            _navigationImpl.AddForTabbedPage(path, tabs);
            return this;
        }

        public Navigation AddForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.AddForTabbedPage<TViewModel>(canNavigate, tabs);
            return this;
        }

        public Navigation AddForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TParameter>(parameter, canNavigate, tabs);
            return this;
        }

        public ResultNavigation<TResult> AddForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            return new ResultNavigation<TResult>(this);
        }

        public ResultNavigation<TResult> AddForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            return new ResultNavigation<TResult>(this);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationImpl.GetPathAndParameters(additionalParameters, additionalQueries);
        }
    }

    public class Navigation<TRootViewModel> : INavigation<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly NavigationImpl _navigationImpl;

        public Navigation(INavigationPath navigationPath)
        {
            _navigationImpl = new NavigationImpl(navigationPath);
        }

        public Navigation<TRootViewModel> Add(string path)
        {
            _navigationImpl.Add(path);
            return this;
        }

        public Navigation<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel>(canNavigate);
            return this;
        }

        public Navigation<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter>(parameter, canNavigate);
            return this;
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel, TResult>(resultReceived, canNavigate);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public Navigation<TRootViewModel> AddForTabbedPage(params ITab[] tabs)
        {
            _navigationImpl.AddForTabbedPage(tabs);
            return this;
        }

        public Navigation<TRootViewModel> AddForTabbedPage(string path, params ITab[] tabs)
        {
            _navigationImpl.AddForTabbedPage(path, tabs);
            return this;
        }

        public Navigation<TRootViewModel> AddForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.AddForTabbedPage<TViewModel>(canNavigate, tabs);
            return this;
        }

        public Navigation<TRootViewModel> AddForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TParameter>(parameter, canNavigate, tabs);
            return this;
        }

        public ResultNavigation<TRootViewModel, TResult> AddForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public ResultNavigation<TRootViewModel, TResult> AddForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.AddForTabbedPage<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationImpl.GetPathAndParameters(additionalParameters, additionalQueries);
        }
    }

    internal class NavigationImpl
    {
        private INavigationPath _rootNavigationPath;
        private INavigationPath _lastNavigationPath;

        public NavigationImpl(INavigationPath navigationPath)
        {
            _rootNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add(string path)
        {
            var navigationPath = new NavigationPath(path);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath<TViewModel>(canNavigate);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath<TViewModel, TParameter>(parameter, canNavigate);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigationPath = new ReceivableNavigationPath<TViewModel, TResult>(resultReceived, canNavigate);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new ReceivableNavigationPath<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage(params ITab[] tabs)
        {
            var navigationPath = new TabbedNavigationPath(tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage(string path, params ITab[] tabs)
        {
            var navigationPath = new TabbedNavigationPath(path, tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new TabbedNavigationPath<TViewModel>(canNavigate, tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new TabbedNavigationPath<TViewModel, TParameter>(parameter, canNavigate, tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new ReceivableTabbedNavigationPath<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void AddForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new ReceivableTabbedNavigationPath<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            var parameters = new NavigationParameters();
            if (additionalParameters != null)
            {
                foreach (var pair in additionalParameters)
                {
                    parameters.Add(pair.Key, pair.Value);
                }
            }

            var path = _rootNavigationPath.GetPath(parameters, additionalQueries);

            return (path, parameters);
        }
    }
}
