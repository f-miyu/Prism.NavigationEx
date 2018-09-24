using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Threading;
namespace Prism.NavigationEx
{
    public abstract class NavigationWithConfirmByViewModel<TConfirmParameter> : NavigationViewModel, INavigationWithConfirmByViewModel<TConfirmParameter>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmByViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        public abstract Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }

    public abstract class NavigationWithConfirmByViewModel<TParameter, TConfirmParameter> : NavigationViewModel<TParameter>, INavigationWithConfirmByViewModel<TParameter, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmByViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        public abstract Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }

    public abstract class NavigationWithConfirmByViewModelResult<TResult, TConfirmParameter> : NavigationViewModelResult<TResult>, INavigationWithConfirmByViewModelResult<TResult, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmByViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        public abstract Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }

    public abstract class NavigationWithConfirmByViewModel<TParameter, TResult, TConfirmParameter> : NavigationViewModel<TParameter, TResult>, INavigationWithConfirmByViewModel<TParameter, TResult, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmByViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter);
        public abstract Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter);
    }
}
