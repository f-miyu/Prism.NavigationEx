using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigationPathBase
    {
        (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null);
    }

    public interface INavigationPathBase<TRootViewModel> : INavigationPathBase where TRootViewModel : INavigationViewModel
    {
    }

    public interface INavigationPath : INavigationPathBase
    {
        INavigationPath Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPath Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
        INavigationPathResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPathResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
    }

    public interface INavigationPathResult<TReceivedResult> : INavigationPathBase
    {
        INavigationPath Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPath Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
        INavigationPathResult<TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPathResult<TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
    }

    public interface INavigationPath<TRootViewModel> : INavigationPathBase<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
        INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
    }

    public interface INavigationPathResult<TRootViewModel, TReceivedResult> : INavigationPathBase<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
        INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPathResult<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
    }
}
