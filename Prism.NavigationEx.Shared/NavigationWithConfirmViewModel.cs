using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Threading;
namespace Prism.NavigationEx
{
    public abstract class NavigationWithConfirmViewModel : NavigationViewModel, INavigationWithConfirmViewModel, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync();
        public abstract Task<bool> CanNavigateAtBackAsync();
    }

    public abstract class NavigationWithConfirmViewModel<TParameter> : NavigationViewModel<TParameter>, INavigationWithConfirmViewModel<TParameter>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync();
        public abstract Task<bool> CanNavigateAtBackAsync();
    }

    public abstract class NavigationWithConfirmViewModelResult<TResult> : NavigationViewModelResult<TResult>, INavigationWithConfirmViewModelResult<TResult>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync();
        public abstract Task<bool> CanNavigateAtBackAsync();
    }

    public abstract class NavigationWithConfirmViewModel<TParameter, TResult> : NavigationViewModel<TParameter, TResult>, INavigationWithConfirmViewModel<TParameter, TResult>, IConfirmNavigationAsync
    {
        protected NavigationWithConfirmViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public abstract Task<bool> CanNavigateAtNewAsync();
        public abstract Task<bool> CanNavigateAtBackAsync();
    }
}
