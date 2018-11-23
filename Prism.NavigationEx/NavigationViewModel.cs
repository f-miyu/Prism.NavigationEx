using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;

namespace Prism.NavigationEx
{
    public abstract class NavigationViewModel : BindableBase, INavigationViewModel
    {
        protected INavigationService NavigationService { get; }
        private Func<INavigationViewModel, Task> _receiveResult;

        protected NavigationViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.TryGetValue<string>(NavigationParameterKey.ReceiveResultId, out var id))
            {
                parameters.TryGetValue<Func<INavigationViewModel, Task>>(id, out _receiveResult);
            }

            if (parameters.GetNavigationMode() == NavigationMode.Back && _receiveResult != null)
            {
                _receiveResult(this);
            }
        }

        public virtual void Destroy()
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            var result = true;
            Func<Task<bool>> canNavigate = null;

            if (!parameters.TryGetValue<Func<Task<bool>>>(NavigationParameterKey.CanNavigate, out canNavigate))
            {
                if (parameters.TryGetValue<string>(NavigationParameterKey.CanNavigateId, out var id))
                {
                    parameters.TryGetValue<Func<Task<bool>>>(id, out canNavigate);
                }
            }

            if (canNavigate != null)
            {
                result = await canNavigate();
            }

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            if (result)
            {
                OnNavigatingFrom(parameters);
            }

            return result;
        }

        protected virtual Task<INavigationResult> NavigateAsync(INavigation navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
        {
            return NavigationService.NavigateAsync(navigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(INavigation<TViewModel> navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TResult>(navigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, cancellationToken);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateAsync(NavigationFactory.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateAsync(NavigationFactory.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TResult>(NavigationFactory.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, cancellationToken);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateAsync<TViewModel, TResult>(NavigationFactory.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, cancellationToken);
        }

        protected virtual Task<INavigationResult> NavigateTabbedPageAsync(bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, params ITab[] tabs)
        {
            return NavigationService.NavigateTabbedPageAsync(useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced, tabs);
        }

        protected virtual Task<INavigationResult> GoBackAsync(bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return NavigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        protected virtual Task<INavigationResult> GoBackToRootAsync(Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return NavigationService.GoBackToRootAsync(parameters);
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
        private TaskCompletionSource<TResult> _tcs;

        protected NavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New &&
                !parameters.CreateTabExists(NavigationNameProvider.GetNavigationName(GetType())))
            {
                if (parameters.TryGetValue<string>(NavigationParameterKey.TaskCompletionSourceId, out var id))
                {
                    parameters.TryGetValue<TaskCompletionSource<TResult>>(id, out _tcs);
                }
            }
        }

        public override void OnNavigatingFrom(INavigationParameters parameters)
        {
            base.OnNavigatingFrom(parameters);

            if (_tcs == null || parameters.GetNavigationMode() == NavigationMode.New)
                return;

            if (parameters.TryGetValue<TResult>(NavigationParameterKey.Result, out var result))
            {
                _tcs.TrySetResult(result);
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

        protected virtual Task<INavigationResult> GoBackAsync(TResult result, bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return NavigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
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