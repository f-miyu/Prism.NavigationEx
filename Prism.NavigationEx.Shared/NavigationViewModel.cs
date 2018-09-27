using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;
using Xamarin.Forms;
using Prism.Events;
using System.Collections.Generic;

namespace Prism.NavigationEx
{
    public abstract class NavigationViewModel : BindableBase, INavigationViewModel
    {
        public INavigationService NavigationService { get; }
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

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                result = await CanNavigateAtBackAsync().ConfigureAwait(false);
            }
            else
            {
                result = await CanNavigateAtNewAsync().ConfigureAwait(false);

                if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
                {
                    cts.Cancel();
                }
            }

            if (result)
            {
                OnNavigatingFrom(parameters);
            }

            return result;
        }

        public virtual Task<bool> CanNavigateAtNewAsync()
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanNavigateAtBackAsync()
        {
            return Task.FromResult(true);
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

            if (parameters.GetNavigationMode() == NavigationMode.New)
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
