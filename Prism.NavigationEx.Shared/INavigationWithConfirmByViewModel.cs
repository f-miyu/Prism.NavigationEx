using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigationWithConfirmByViewModel<TConfirmParameter> : INavigationViewModel
    {
        Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }

    public interface INavigationWithConfirmByViewModel<TParameter, TConfirmParameter> : INavigationViewModel<TParameter>, INavigationWithConfirmByViewModel<TConfirmParameter>
    {
    }

    public interface INavigationWithConfirmByViewModelResult<TResult, TConfirmParameter> : INavigationViewModelResult<TResult>, INavigationWithConfirmByViewModel<TConfirmParameter>
    {
    }

    public interface INavigationWithConfirmByViewModel<TParameter, TResult, TConfirmParameter> : INavigationViewModel<TParameter, TResult>, INavigationWithConfirmByViewModel<TParameter, TConfirmParameter>, INavigationWithConfirmByViewModelResult<TResult, TConfirmParameter>
    {
    }
}
