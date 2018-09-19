using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public abstract class NavigationViewModel : BindableBase, INavigationAware, IDestructible
    {
        public static readonly string ParameterKey = "parameter";
        public static readonly string TaskCompletionSourceKey = "taskCompletionSource";

        protected INavigationService NavigationService { get; }

        protected NavigationViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }
    }

    public abstract class NavigationViewModel<TParameer> : NavigationViewModel
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.ContainsKey(ParameterKey))
            {
                var parameter = (TParameer)parameters[ParameterKey];

                Prepare(parameter);
            }
        }

        public abstract void Prepare(TParameer parameer);
    }

    public abstract class NavigationViewModelResult<TResult> : NavigationViewModel
    {
        private TaskCompletionSource<INavigationResult<TResult>> _tcs;

        protected NavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.ContainsKey(TaskCompletionSourceKey))
            {
                _tcs = (TaskCompletionSource<INavigationResult<TResult>>)parameters[TaskCompletionSourceKey];
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (_tcs == null || parameters.GetNavigationMode() == NavigationMode.New)
                return;

            if (parameters.ContainsKey(ParameterKey))
            {
                var parameter = (TResult)parameters[ParameterKey];

                _tcs.TrySetResult(new NavigationResult<TResult>(true, parameter));
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

    public abstract class NavigationViewModel<TParameer, TResult> : NavigationViewModel<TResult>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.ContainsKey(ParameterKey))
            {
                var parameter = (TParameer)parameters[ParameterKey];

                Prepare(parameter);
            }
        }

        public abstract void Prepare(TParameer parameer);
    }
}
