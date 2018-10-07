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
        public Func<Task<bool>> CanNavigate { get; set; }
        public IEnumerable<ITab> Tabs { get; set; }
        public Type ViewModelType { get; set; }
        public string Path { get; set; }

        protected bool _isParameterSet;
        private object _parameter;

        public object Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value;
                _isParameterSet = true;
            }
        }

        public virtual string GetPath(INavigationParameters parameters, INavigationParameters queries = null, INavigationParameters nextQueries = null)
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

            if (_isParameterSet)
            {
                var parameterId = Guid.NewGuid().ToString();
                queries.Add(NavigationParameterKey.ParameterId, parameterId);
                parameters.Add(parameterId, Parameter);
            }

            if (Tabs != null)
            {
                foreach (var tab in Tabs)
                {
                    queries.Add(KnownNavigationParameters.CreateTab, tab.GetPath(ref parameters));
                }
            }

            var path = Path ?? string.Empty;
            if (ViewModelType != null)
            {
                path = NavigationNameProvider.GetNavigationName(ViewModelType);
            }

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

    public class NavigationPath<TResult> : NavigationPath
    {
        public ResultReceivedDelegate<TResult> ResultReceived { get; set; }
        protected TaskCompletionSource<TResult> _tcs;

        public override string GetPath(INavigationParameters parameters, INavigationParameters queries = null, INavigationParameters nextQueries = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (NextNavigationPath != null)
            {
                if (queries == null)
                {
                    queries = new NavigationParameters();
                }

                if (nextQueries == null)
                {
                    nextQueries = new NavigationParameters();
                }

                Func<INavigationViewModel, Task> receiveResult = ReceiveResultAsync;
                var receiveResultId = Guid.NewGuid().ToString();
                queries.Add(NavigationParameterKey.ReceiveResultId, receiveResultId);
                parameters.Add(receiveResultId, receiveResult);

                if (_tcs != null && !_tcs.Task.IsCompleted)
                {
                    _tcs.TrySetCanceled();
                }

                _tcs = new TaskCompletionSource<TResult>();
                var taskCompletionSourceId = Guid.NewGuid().ToString();
                nextQueries.Add(NavigationParameterKey.TaskCompletionSourceId, taskCompletionSourceId);
                parameters.Add(taskCompletionSourceId, _tcs);
            }

            return base.GetPath(parameters, queries, nextQueries);
        }

        private async Task ReceiveResultAsync(INavigationViewModel viewModel)
        {
            if (_tcs == null) return;

            try
            {
                var result = await _tcs.Task.ConfigureAwait(false);
                ResultReceived?.Invoke(viewModel, new NavigationResult<TResult>(true, result));
            }
            catch (Exception e)
            {
                ResultReceived?.Invoke(viewModel, new NavigationResult<TResult>(false, default(TResult), e));
            }
        }
    }
}
