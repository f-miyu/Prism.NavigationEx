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
        public static readonly string ParameterIdKey = "parameterId";

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

    public abstract class NavigationViewModel<TParameer> : NavigationViewModel
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.TryGetValue<string>(ParameterIdKey, out var id))
                {
                    if (parameters.TryGetValue<TParameer>(id, out var parameter))
                    {
                        Prepare(parameter);
                    }
                }
                else if (parameters.TryGetValue<TParameer>(ParameterKey, out var parameter))
                {
                    Prepare(parameter);
                }
            }
        }

        public abstract void Prepare(TParameer parameer);
    }

    public abstract class NavigationViewModelResult<TResult> : NavigationViewModel
    {
        private TaskCompletionSource<INavigationResult<TResult>> _tcs;

        public string ResultParameterKey { get; } = Guid.NewGuid().ToString();

        protected NavigationViewModelResult(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.ContainsKey(TaskCompletionSourceKey))
            {
                _tcs = (TaskCompletionSource<INavigationResult<TResult>>)parameters[TaskCompletionSourceKey];
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

    public abstract class NavigationViewModel<TParameer, TResult> : NavigationViewModelResult<TResult>
    {
        protected NavigationViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.TryGetValue<string>(ParameterIdKey, out var id))
                {
                    if (parameters.TryGetValue<TParameer>(id, out var parameter))
                    {
                        Prepare(parameter);
                    }
                }
                else if (parameters.TryGetValue<TParameer>(ParameterKey, out var parameter))
                {
                    Prepare(parameter);
                }
            }
        }

        public abstract void Prepare(TParameer parameer);
    }
}
