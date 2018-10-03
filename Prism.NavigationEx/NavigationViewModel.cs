using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;
using Xamarin.Forms;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;

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

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
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

        public virtual async Task<bool> CanNavigateAsync(NavigationParameters parameters)
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
    }

    public abstract class NavigationViewModel<TParameter> : NavigationViewModel, INavigationViewModel<TParameter>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
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

        public override void OnNavigatingTo(NavigationParameters parameters)
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

        public override void OnNavigatingFrom(NavigationParameters parameters)
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

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            this.PrepareIfNeeded(parameters);
        }

        public abstract void Prepare(TParameter parameter);
    }
}