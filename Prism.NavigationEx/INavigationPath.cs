using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigationPathBase<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, IDictionary<string, string> additionalPathParameters = null);
    }

    public interface INavigationPath<TRootViewModel> : INavigationPathBase<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
        INavigationPath<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel;
        INavigationPath<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>;
    }

    public interface INavigationPath<TRootViewModel, TReceivedResult> : INavigationPathBase<TRootViewModel> where TRootViewModel : INavigationViewModel
    {
        INavigationPath<TRootViewModel> Add<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPath<TRootViewModel> Add<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
        INavigationPath<TRootViewModel, TResult> Add<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationPath<TRootViewModel, TResult> Add<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
    }
}
