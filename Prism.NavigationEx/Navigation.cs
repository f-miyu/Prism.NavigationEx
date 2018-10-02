using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public class Navigation : INavigation
    {
        public INavigation NextNavigation { get; set; }
        public string Path { get; }

        public Navigation(string path)
        {
            Path = path;
        }

        public string CreateNavigationPath(NavigationParameters parameters, NavigationParameters pathParameters = null, NavigationParameters nextPathParameters = null)
        {
            var path = Path ?? string.Empty;

            if (NextNavigation != null)
            {
                path += "/" + NextNavigation.CreateNavigationPath(parameters, nextPathParameters);
            }

            return path;
        }
    }

    public class Navigation<TViewModel> : INavigation where TViewModel : INavigationViewModel
    {
        public INavigation NextNavigation { get; set; }
        public Func<Task<bool>> CanNavigate { get; }

        public Navigation(Func<Task<bool>> canNavigate)
        {
            CanNavigate = canNavigate;
        }

        public virtual string CreateNavigationPath(NavigationParameters parameters, NavigationParameters pathParameters = null, NavigationParameters nextPathParameters = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (pathParameters == null)
            {
                pathParameters = new NavigationParameters();
            }

            if (CanNavigate != null)
            {
                var canNavigateId = Guid.NewGuid().ToString();
                pathParameters.Add(NavigationParameterKey.CanNavigateId, canNavigateId);
                parameters.Add(canNavigateId, CanNavigate);
            }

            var path = NavigationNameProvider.GetNavigationName(typeof(TViewModel));

            if (pathParameters.Count > 0)
            {
                path += "?" + string.Join("&", pathParameters.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigation != null)
            {
                path += "/" + NextNavigation.CreateNavigationPath(parameters, nextPathParameters);
            }

            return path;
        }
    }

    public class Navigation<TViewModel, TParameter> : Navigation<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public Navigation(TParameter parameter, Func<Task<bool>> canNavigate) : base(canNavigate)
        {
            Parameter = parameter;
        }

        public override string CreateNavigationPath(NavigationParameters parameters, NavigationParameters pathParameters = null, NavigationParameters nextPathParameters = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (pathParameters == null)
            {
                pathParameters = new NavigationParameters();
            }

            var parameterId = Guid.NewGuid().ToString();
            pathParameters.Add(NavigationParameterKey.ParameterId, parameterId);
            parameters.Add(parameterId, Parameter);

            return base.CreateNavigationPath(parameters, pathParameters, nextPathParameters);
        }
    }
}
