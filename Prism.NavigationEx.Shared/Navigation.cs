using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public class Navigation<TViewModel> : INavigation, INavigation<TViewModel> where TViewModel : INavigationViewModel
    {
        public Type ViewModelType => typeof(TViewModel);

        public INavigation NextNavigation { get; set; }

        public virtual string CreateNavigationPath(INavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var path = NavigationNameProvider.GetNavigationName(typeof(TViewModel));

            if (pathParameters != null && pathParameters.Count > 0)
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
        protected TParameter Parameter { get; private set; }

        public Navigation(TParameter parameter)
        {
            Parameter = parameter;
        }

        public override string CreateNavigationPath(INavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

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
