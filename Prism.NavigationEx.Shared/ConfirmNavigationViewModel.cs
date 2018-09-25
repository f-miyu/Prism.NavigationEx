using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Threading;
namespace Prism.NavigationEx
{
    public abstract class ConfirmNavigationViewModel<TConfirmParameter> : NavigationViewModel, IConfirmNavigationViewModel<TConfirmParameter>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public virtual Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtNewAsync();
        }

        public virtual Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtBackAsync();
        }
    }

    public abstract class ConfirmNavigationViewModel<TParameter, TConfirmParameter> : NavigationViewModel<TParameter>, IConfirmNavigationViewModel<TParameter, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public virtual Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtNewAsync();
        }

        public virtual Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtBackAsync();
        }
    }

    public abstract class ConfirmNavigationViewModelResult<TResult, TConfirmParameter> : NavigationViewModelResult<TResult>, IConfirmNavigationViewModelResult<TResult, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public virtual Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtNewAsync();
        }

        public virtual Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtBackAsync();
        }
    }

    public abstract class ConfirmNavigationViewModel<TParameter, TResult, TConfirmParameter> : NavigationViewModel<TParameter, TResult>, IConfirmNavigationViewModel<TParameter, TResult, TConfirmParameter>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return this.CanNavigateInternalAsync(parameters);
        }

        public virtual Task<bool> CanNavigateAtNewAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtNewAsync();
        }

        public virtual Task<bool> CanNavigateAtBackAsync(TConfirmParameter parameter)
        {
            return CanNavigateAtBackAsync();
        }
    }
}
