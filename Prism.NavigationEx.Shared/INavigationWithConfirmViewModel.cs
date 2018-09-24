using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigationWithConfirmViewModel : INavigationViewModel
    {
        Task<bool> CanNavigateAtNewAsync();
        Task<bool> CanNavigateAtBackAsync();
    }

    public interface INavigationWithConfirmViewModel<TParameter> : INavigationViewModel<TParameter>, INavigationWithConfirmViewModel
    {
    }

    public interface INavigationWithConfirmViewModelResult<TResult> : INavigationViewModelResult<TResult>, INavigationWithConfirmViewModel
    {
    }

    public interface INavigationWithConfirmViewModel<TParameter, TResult> : INavigationViewModel<TParameter, TResult>, INavigationWithConfirmViewModel<TParameter>, INavigationWithConfirmViewModelResult<TResult>
    {
    }

}
