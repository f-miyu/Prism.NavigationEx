using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class TabNavigation
    {
        public string Name { get; }
        public bool WrapInNavigationPage { get; }

        public TabNavigation(string name, bool wrapInNavigationPage)
        {
            Name = name;
            WrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string CreateTabParameter(NavigationParameters parameters)
        {
            return $"{(WrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{Name}";
        }
    }

    public class TabNavigation<TViewModel> : TabNavigation where TViewModel : INavigationViewModel
    {
        public TabNavigation(bool wrapInNavigationPage) : base(NavigationNameProvider.GetNavigationName(typeof(TViewModel)), wrapInNavigationPage)
        {
        }
    }

    public class TabNavigation<TViewModel, TParameter> : TabNavigation<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public TabNavigation(TParameter parameter, bool wrapInNavigationPage) : base(wrapInNavigationPage)
        {
            Parameter = parameter;
        }

        public override string CreateTabParameter(NavigationParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var parameter = base.CreateTabParameter(parameters);

            var parameterId = Guid.NewGuid().ToString();
            parameter += $"?{NavigationParameterKey.ParameterId}={parameterId}";
            parameters.Add(parameterId, Parameter);

            return parameter;
        }
    }
}
