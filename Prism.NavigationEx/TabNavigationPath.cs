using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class TabNavigationPath : ITabNavigationPath
    {
        public string Name { get; }
        public bool WrapInNavigationPage { get; }

        public TabNavigationPath(string name, bool wrapInNavigationPage)
        {
            Name = name;
            WrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string CreateTabParameter(NavigationParameters parameters)
        {
            return $"{(WrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{Name}";
        }
    }

    public class TabNavigationPath<TViewModel> : TabNavigationPath where TViewModel : INavigationViewModel
    {
        public TabNavigationPath(bool wrapInNavigationPage) : base(NavigationNameProvider.GetNavigationName(typeof(TViewModel)), wrapInNavigationPage)
        {
        }
    }

    public class TabNavigationPath<TViewModel, TParameter> : TabNavigationPath<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public TabNavigationPath(TParameter parameter, bool wrapInNavigationPage) : base(wrapInNavigationPage)
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
