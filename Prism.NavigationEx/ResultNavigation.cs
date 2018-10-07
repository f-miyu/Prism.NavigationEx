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

        public Navigation Add<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel>(canNavigate, tabs);
        }

        public Navigation Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter>(parameter, canNavigate, tabs);
        }

        public ResultNavigation<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel, TResult>(resultReceived, canNavigate, tabs);
        }

        public ResultNavigation<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null)
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

        public Navigation<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel>(canNavigate, tabs);
        }

        public Navigation<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter>(parameter, canNavigate, tabs);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigation.Add<TViewModel, TResult>(resultReceived, canNavigate, tabs);
        }

        public ResultNavigation<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigation.Add<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
        }

        public (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null)
        {
            return _navigation.GetPathAndParameters(additionalParameters, additionalQueries);
        }
    }
}
