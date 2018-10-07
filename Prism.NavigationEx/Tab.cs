using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class Tab : ITab
    {
        protected string _name;
        protected bool _wrapInNavigationPage;

        public Tab(string name, bool wrapInNavigationPage = false)
        {
            _name = name;
            _wrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string GetPath(ref INavigationParameters parameters)
        {
            return $"{(_wrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{_name}";
        }
    }

    public class Tab<TViewModel> : ITab where TViewModel : INavigationViewModel
    {
        protected bool _wrapInNavigationPage;

        public Tab(bool wrapInNavigationPage = false)
        {
            _wrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string GetPath(ref INavigationParameters parameters)
        {
            var name = NavigationNameProvider.GetNavigationName(typeof(TViewModel));
            return $"{(_wrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{name}";
        }
    }

    public class Tab<TViewModel, TParameter> : Tab<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        protected TParameter _parameter;

        public Tab(TParameter parameter, bool wrapInNavigationPage = false) : base(wrapInNavigationPage)
        {
            _parameter = parameter;
        }

        public override string GetPath(ref INavigationParameters parameters)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            var path = base.GetPath(ref parameters);

            var parameterId = Guid.NewGuid().ToString();
            path += $"?{NavigationParameterKey.ParameterId}={parameterId}";
            parameters.Add(parameterId, _parameter);

            return path;
        }
    }
}
