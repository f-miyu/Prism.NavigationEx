using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;

namespace Prism.NavigationEx
{
    public abstract class NavigationViewModel : BindableBase, INavigationViewModel, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }

        protected NavigationViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateAsync<TViewModel>(useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateAsync<TViewModel, TParameter>(parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TResult>(useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TParameter, TResult>(parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateAsync<TViewModel>(useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateAsync<TViewModel, TParameter>(parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TResult>(useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TParameter, TResult>(parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> GoBackAsync(bool? useModalNavigation = null, bool animated = true)
        {
            return NavigationService.GoBackAsync(useModalNavigation: useModalNavigation, animated: animated);
        }

        protected virtual Task<INavigationResult> GoBackToRootAsync()
        {
            return NavigationService.GoBackToRootAsync();
        }
    }

    public abstract class NavigationViewModel<TParameter> : NavigationViewModel, INavigationViewModel<TParameter>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            this.PrepareIfNeeded(parameters);
        }

        public abstract void Prepare(TParameter parameter);
    }

    public abstract class NavigationViewModelResult<TResult> : NavigationViewModel, INavigationViewModelResult<TResult>
    {
        private TaskCompletionSource<INavigationResult<TResult>> _tcs;
        private string _resultParameterKey = Guid.NewGuid().ToString();

        protected NavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (!parameters.TryGetValue<TaskCompletionSource<INavigationResult<TResult>>>(NavigationParameterKey.TaskCompletionSource, out _tcs))
                {
                    if (parameters.TryGetValue<string>(NavigationParameterKey.TaskCompletionSourceId, out var id))
                    {
                        parameters.TryGetValue<TaskCompletionSource<INavigationResult<TResult>>>(id, out _tcs);
                    }
                }
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (_tcs == null || parameters.GetNavigationMode() == NavigationMode.New)
                return;

            if (parameters.TryGetValue<TResult>(_resultParameterKey, out var result))
            {
                _tcs.TrySetResult(new NavigationResult<TResult>(true, result));
            }
            else
            {
                _tcs.TrySetCanceled();
            }
        }

        public override void Destroy()
        {
            base.Destroy();

            _tcs?.TrySetCanceled();
        }

        protected virtual Task<INavigationResult> GoBackAsync(TResult result, bool? useModalNavigation = null, bool animated = true)
        {
            return NavigationService.GoBackAsync(result, _resultParameterKey, useModalNavigation, animated);
        }

        protected virtual Task<INavigationResult> GoBackToRootAsync(TResult result)
        {
            return NavigationService.GoBackToRootAsync(result, _resultParameterKey);
        }
    }

    public abstract class NavigationViewModel<TParameter, TResult> : NavigationViewModelResult<TResult>, INavigationViewModel<TParameter, TResult>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            this.PrepareIfNeeded(parameters);
        }

        public abstract void Prepare(TParameter parameter);
    }
}
