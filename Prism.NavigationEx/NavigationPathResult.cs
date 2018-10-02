using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class NavigationPathResult<TReceivedResult> : INavigationPath
    {
        private readonly NavigationPath _navigationPath;

        public NavigationPathResult(NavigationPath navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public NavigationPath Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>(canNavigate);
        }

        public NavigationPath Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public NavigationPathResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public NavigationPathResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null)
        {
            return _navigationPath.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }

    public class NavigationPathResult<TRootViewModel, TReceivedResult> : INavigationPath<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly NavigationPath<TRootViewModel> _navigationPath;

        public NavigationPathResult(NavigationPath<TRootViewModel> navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public NavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>(canNavigate);
        }

        public NavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public NavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public NavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null)
        {
            return _navigationPath.GetPathAndParameters(additionalParameters, additionalPathParameters);
        }
    }
}
