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
        protected string _path;

        public NavigationPath(string path)
        {
            _path = path;
        }

        public virtual string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            var path = _path ?? string.Empty;

            if (queries != null && queries.Count > 0)
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

    public class NavigationPath<TViewModel> : INavigationPath where TViewModel : INavigationViewModel
    {
        public INavigationPath NextNavigationPath { get; set; }
        protected Func<Task<bool>> _canNavigate;

        public NavigationPath(Func<Task<bool>> canNavigate = null)
        {
            _canNavigate = canNavigate;
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

            if (_canNavigate != null)
            {
                var canNavigateId = Guid.NewGuid().ToString();
                queries.Add(NavigationParameterKey.CanNavigateId, canNavigateId);
                parameters.Add(canNavigateId, _canNavigate);
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
        protected TParameter _parameter;

        public NavigationPath(TParameter parameter, Func<Task<bool>> canNavigate = null) : base(canNavigate)
        {
            _parameter = parameter;
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
            parameters.Add(parameterId, _parameter);

            return base.GetPath(parameters, queries, nextQueries);
        }
    }
}
