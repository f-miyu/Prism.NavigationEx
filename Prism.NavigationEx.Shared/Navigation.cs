﻿using System;
namespace Prism.NavigationEx
{
    public class Navigation<TViewModel> : INavigation where TViewModel : INavigationViewModel
    {
        public Type ViewModelType => typeof(TViewModel);

        private object _parameter;
        public object Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                ParameterExists = true;
            }
        }

        public bool ParameterExists { get; private set; }
        public virtual Type ResultType => null;
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

    public class NavigationResult<TViewModel, TResult> : Navigation<TViewModel> where TViewModel : INavigationViewModelResult<TResult>
    {
        public override Type ResultType => typeof(TResult);
    }

    public class Navigation<TViewModel, TParameter, TResult> : NavigationResult<TViewModel, TResult> where TViewModel : INavigationViewModel<TParameter, TResult>
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
