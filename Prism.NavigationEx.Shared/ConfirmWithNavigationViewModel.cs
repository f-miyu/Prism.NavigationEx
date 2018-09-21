using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Threading;
namespace Prism.NavigationEx
{
    public abstract class ConfirmWitnNavigationViewModel<TConfirmParameter> : NavigationViewModel, IConfirmNavigationAsync
    {
        protected ConfirmWitnNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);
            var result = await CanNavigateAsync(parameter).ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync(TConfirmParameter parameter);

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }
    }

    public abstract class ConfirmWitnNavigationViewModel<TParameter, TConfirmParameter> : NavigationViewModel<TParameter>, IConfirmNavigationAsync
    {
        protected ConfirmWitnNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);
            var result = await CanNavigateAsync(parameter).ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync(TConfirmParameter parameter);

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }
    }

    public abstract class ConfirmWitnNavigationViewModelResult<TResult, TConfirmParameter> : NavigationViewModelResult<TResult>, IConfirmNavigationAsync
    {
        protected ConfirmWitnNavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);
            var result = await CanNavigateAsync(parameter).ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync(TConfirmParameter parameter);

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }
    }

    public abstract class ConfirmWitnNavigationViewModel<TParameter, TResult, TConfirmParameter> : NavigationViewModel<TParameter, TResult>, IConfirmNavigationAsync
    {
        protected ConfirmWitnNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);
            var result = await CanNavigateAsync(parameter).ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync(TConfirmParameter parameter);

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        protected virtual Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return NavigationService.NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }
    }
}
