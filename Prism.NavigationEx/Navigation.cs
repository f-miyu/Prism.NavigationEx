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

        public Navigation Add(string path, params ITab[] tabs)
        {
            _navigationImpl.Add(path, tabs);
            return this;
        }

        public Navigation Add<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel>(canNavigate, tabs);
            return this;
        }

        public Navigation Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter>(parameter, canNavigate, tabs);
            return this;
        }

        public ResultNavigation<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            return new ResultNavigation<TResult>(this);
        }

        public ResultNavigation<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            return new ResultNavigation<TResult>(this);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null)
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

        public Navigation<TRootViewModel> Add(string path, params ITab[] tabs)
        {
            _navigationImpl.Add(path, tabs);
            return this;
        }

        public Navigation<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel>(canNavigate, tabs);
            return this;
        }

        public Navigation<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter>(parameter, canNavigate, tabs);
            return this;
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            _navigationImpl.Add<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            _navigationImpl.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            return new ResultNavigation<TRootViewModel, TResult>(this);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null)
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

        public void Add(string path, params ITab[] tabs)
        {
            var navigationPath = new NavigationPath
            {
                Path = path,
                Tabs = tabs
            };
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath
            {
                ViewModelType = typeof(TViewModel),
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath
            {
                ViewModelType = typeof(TViewModel),
                Parameter = parameter,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath<TResult>
            {
                ViewModelType = typeof(TViewModel),
                ResultReceived = resultReceived,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public void Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath<TResult>
            {
                ViewModelType = typeof(TViewModel),
                Parameter = parameter,
                ResultReceived = resultReceived,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            _lastNavigationPath.NextNavigationPath = navigationPath;
            _lastNavigationPath = navigationPath;
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null)
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
