using System;
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
}
