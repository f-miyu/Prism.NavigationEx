using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public class NavigationPath : INavigationPath
    {
        public INavigationPath NextNavigationPath { get; set; }
        public string Path { get; }

        public NavigationPath(string path)
        {
            Path = path;
        }

        public string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            var path = Path ?? string.Empty;

            if (NextNavigationPath != null)
            {
                path += "/" + NextNavigationPath.GetPath(parameters, nextQueries);
            }

            return path;
        }
    }

    public class NavigationPath<TViewModel> : INavigationPath where TViewModel : INavigationViewModel
    {
        public INavigationPath NextNavigationPath { get; set; }
        public Func<Task<bool>> CanNavigate { get; }

        public NavigationPath(Func<Task<bool>> canNavigate)
        {
            CanNavigate = canNavigate;
        }

        public virtual string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
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

            var path = NavigationNameProvider.GetNavigationName(typeof(TViewModel));

            if (queries.Count > 0)
            {
                path += "?" + string.Join("&", queries.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigationPath != null)
            {
                path += "/" + NextNavigationPath.GetPath(parameters, nextQueries);
            }

            return path;
        }
    }

    public class NavigationPath<TViewModel, TParameter> : NavigationPath<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public NavigationPath(TParameter parameter, Func<Task<bool>> canNavigate) : base(canNavigate)
        {
            Parameter = parameter;
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
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

            return base.GetPath(parameters, queries, nextQueries);
        }
    }
}
