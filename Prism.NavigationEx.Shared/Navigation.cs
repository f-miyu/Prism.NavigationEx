using System;
using Prism.Navigation;
namespace Prism.NavigationEx
{
    public class Navigation<TViewModel> : INavigation where TViewModel : INavigationViewModel
    {
        public Type ViewModelType => typeof(TViewModel);

        private object _parameter;
        public object Parameter
        {
            get => _parameter;
            protected set
            {
                _parameter = value;
                ParameterExists = true;
            }
        }

        public bool ParameterExists { get; private set; }

        public virtual void OnResult(INavigationViewModel viewModel, INavigationParameters parameters)
        {
        }
    }

    public class Navigation<TViewModel, TParameter> : Navigation<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        private TParameter _parameter;
        public new TParameter Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                base.Parameter = value;
            }
        }
    }

    public class NavigationResult<TViewModel, TResult> : Navigation<TViewModel> where TViewModel : INavigationViewModel
    {
        public event EventHandler<ResultEventArgs<TViewModel, TResult>> Result;

        public override void OnResult(INavigationViewModel viewModel, INavigationParameters parameters)
        {
            if (Result != null && parameters.TryGetValue<TResult>(NavigationParameterKey.Result, out var result))
            {
                Result(this, new ResultEventArgs<TViewModel, TResult>((TViewModel)viewModel, result));
            }
        }
    }

    public class Navigation<TViewModel, TParameter, TResult> : NavigationResult<TViewModel, TResult> where TViewModel : INavigationViewModel<TParameter>
    {
        private TParameter _parameter;
        public new TParameter Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                base.Parameter = value;
            }
        }
    }
}
