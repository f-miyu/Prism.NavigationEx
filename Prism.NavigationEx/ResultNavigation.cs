using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class ResultNavigation<TReceivedResult> : INavigation
    {
        private readonly Navigation _navigation;

        public ResultNavigation(Navigation navigation)
        {
            _navigation = navigation;
        }

        public Navigation Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel>(canNavigate);
        }

        public Navigation Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public ResultNavigation<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public ResultNavigation<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public Navigation AddForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel>(canNavigate, tabs);
        }

        public Navigation AddForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TParameter>(parameter, canNavigate, tabs);
        }

        public ResultNavigation<TResult> AddForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TResult>(resultReceived, canNavigate, tabs);
        }

        public ResultNavigation<TResult> AddForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigation.GetPathAndParameters(additionalParameters, additionalQueries);
        }
    }

    public class ResultNavigation<TRootViewModel, TReceivedResult> : INavigation<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        private readonly Navigation<TRootViewModel> _navigation;

        public ResultNavigation(Navigation<TRootViewModel> navigation)
        {
            _navigation = navigation;
        }

        public Navigation<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel>(canNavigate);
        }

        public Navigation<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter>(parameter, canNavigate);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel, TResult>(resultReceived, canNavigate);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
        }

        public Navigation<TRootViewModel> AddForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel>(canNavigate, tabs);
        }

        public Navigation<TRootViewModel> AddForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TParameter>(parameter, canNavigate, tabs);
        }

        public ResultNavigation<TRootViewModel, TResult> AddForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TResult>(resultReceived, canNavigate, tabs);
        }

        public ResultNavigation<TRootViewModel, TResult> AddForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.AddForTabbedPage<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
        }

        public (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null)
        {
            return _navigation.GetPathAndParameters(additionalParameters, additionalQueries);
        }
    }
}
