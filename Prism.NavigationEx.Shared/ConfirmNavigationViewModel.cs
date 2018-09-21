using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Threading;
namespace Prism.NavigationEx
{
    public abstract class ConfirmNavigationViewModel : NavigationViewModel, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            var result = await CanNavigateAsync().ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync();
    }

    public abstract class ConfirmNavigationViewModel<TParameter> : NavigationViewModel<TParameter>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            var result = await CanNavigateAsync().ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync();
    }

    public abstract class ConfirmNavigationViewModelResult<TResult> : NavigationViewModelResult<TResult>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            var result = await CanNavigateAsync().ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync();
    }

    public abstract class ConfirmNavigationViewModel<TParameter, TResult> : NavigationViewModel<TParameter, TResult>, IConfirmNavigationAsync
    {
        protected ConfirmNavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public virtual async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            var result = await CanNavigateAsync().ConfigureAwait(false);

            if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
            {
                cts.Cancel();
            }

            return result;
        }

        protected abstract Task<bool> CanNavigateAsync();
    }
}
