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
        public string Uri { get; }

        public Navigation(string uri)
        {
            Uri = uri;
        }

        public string CreateNavigationUri(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            var uri = Uri ?? string.Empty;

            if (NextNavigation != null)
            {
                uri += "/" + NextNavigation.CreateNavigationUri(parameters, nextQueries);
            }

            return uri;
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

        public virtual string CreateNavigationUri(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (queries == null)
            {
                queries = new NavigationParameters();
            }

            if (CanNavigate != null)
            {
                var canNavigateId = Guid.NewGuid().ToString();
                queries.Add(NavigationParameterKey.CanNavigateId, canNavigateId);
                parameters.Add(canNavigateId, CanNavigate);
            }

            var uri = NavigationNameProvider.GetNavigationName(typeof(TViewModel));

            if (queries.Count > 0)
            {
                uri += "?" + string.Join("&", queries.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigation != null)
            {
                uri += "/" + NextNavigation.CreateNavigationUri(parameters, nextQueries);
            }

            return uri;
        }
    }

    public class Navigation<TViewModel, TParameter> : Navigation<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public Navigation(TParameter parameter, Func<Task<bool>> canNavigate) : base(canNavigate)
        {
            Parameter = parameter;
        }

        public override string CreateNavigationUri(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (queries == null)
            {
                queries = new NavigationParameters();
            }

            var parameterId = Guid.NewGuid().ToString();
            queries.Add(NavigationParameterKey.ParameterId, parameterId);
            parameters.Add(parameterId, Parameter);

            return base.CreateNavigationUri(parameters, queries, nextQueries);
        }
    }
}
