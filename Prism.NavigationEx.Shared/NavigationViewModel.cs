using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;

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
    }

    public abstract class NavigationViewModel<TParameter> : NavigationViewModel, INavigationViewModel<TParameter>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            this.PrepareIfNeeded<TParameter>(parameters);
        }

        public abstract void Prepare(TParameter parameter);
    }

    public abstract class NavigationViewModelResult<TResult> : NavigationViewModel, INavigationViewModelResult<TResult>
    {
        private TaskCompletionSource<INavigationResult<TResult>> _tcs;

        public string ResultParameterKey { get; } = Guid.NewGuid().ToString();

        protected NavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                parameters.TryGetValue<TaskCompletionSource<INavigationResult<TResult>>>(NavigationParameterKey.TaskCompletionSource, out _tcs);
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (_tcs == null || parameters.GetNavigationMode() == NavigationMode.New)
                return;

            if (parameters.TryGetValue<TResult>(ResultParameterKey, out var result))
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
    }

    public abstract class NavigationViewModel<TParameter, TResult> : NavigationViewModelResult<TResult>, INavigationViewModel<TParameter, TResult>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            this.PrepareIfNeeded<TParameter>(parameters);
        }

        public abstract void Prepare(TParameter parameter);
    }
}
