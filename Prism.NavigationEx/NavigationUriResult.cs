using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class NavigationUriResult<TReceivedResult> : INavigationUri
    {
        private readonly NavigationUri _navigationPath;

        public NavigationUriResult(NavigationUri navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public NavigationUri Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>(canNavigate);
        }

        public NavigationUri Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public NavigationUriResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public NavigationUriResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationPath.GetUriAndParameters(additionalParameters, additionalQueries);
        }
    }

    public class NavigationUriResult<TRootViewModel, TReceivedResult> : INavigationUri<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly NavigationUri<TRootViewModel> _navigationPath;

        public NavigationUriResult(NavigationUri<TRootViewModel> navigationPath)
        {
            _navigationPath = navigationPath;
        }

        public NavigationUri<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel>(canNavigate);
        }

        public NavigationUri<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public NavigationUriResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public NavigationUriResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationPath.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigationPath.GetUriAndParameters(additionalParameters, additionalQueries);
        }
    }
}
