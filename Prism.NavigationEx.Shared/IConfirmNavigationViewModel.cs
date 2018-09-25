using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface IConfirmNavigationViewModel<TConfirmParameter> : INavigationViewModel
    {
        Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }

    public interface IConfirmNavigationViewModel<TParameter, TConfirmParameter> : INavigationViewModel<TParameter>, IConfirmNavigationViewModel<TConfirmParameter>
    {
    }

    public interface IConfirmNavigationViewModelResult<TResult, TConfirmParameter> : INavigationViewModelResult<TResult>, IConfirmNavigationViewModel<TConfirmParameter>
    {
    }

    public interface IConfirmNavigationViewModel<TParameter, TResult, TConfirmParameter> : INavigationViewModel<TParameter, TResult>, IConfirmNavigationViewModel<TParameter, TConfirmParameter>, IConfirmNavigationViewModelResult<TResult, TConfirmParameter>
    {
    }
}
