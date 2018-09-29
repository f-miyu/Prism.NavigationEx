using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public class Navigation<TViewModel> : INavigation where TViewModel : INavigationViewModel
    {
        public INavigation NextNavigation { get; set; }
        public Func<Task<bool>> CanNavigate { get; set; }

        public virtual string CreateNavigationPath(NavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (pathParameters == null)
            {
                pathParameters = new Dictionary<string, string>();
            }

            if (CanNavigate != null)
            {
                var canNavigateId = Guid.NewGuid().ToString();
                pathParameters[NavigationParameterKey.CanNavigateId] = canNavigateId;
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
        public TParameter Parameter { get; set; }

        public override string CreateNavigationPath(NavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (pathParameters == null)
            {
                pathParameters = new Dictionary<string, string>();
            }

            var parameterId = Guid.NewGuid().ToString();
            pathParameters[NavigationParameterKey.ParameterId] = parameterId;
            parameters.Add(parameterId, Parameter);

            return base.CreateNavigationPath(parameters, pathParameters, nextPathParameters);
        }
    }
}
