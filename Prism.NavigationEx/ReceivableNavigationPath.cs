using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class ReceivableNavigationPath<TViewModel, TResult> : NavigationPath<TViewModel> where TViewModel : INavigationViewModel
    {
        public ResultReceivedDelegate<TViewModel, TResult> ResultReceived { get; }

        protected TaskCompletionSource<TResult> _tcs;

        public ReceivableNavigationPath(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate) : base(canNavigate)
        {
            ResultReceived = resultReceived;
        }

        public async Task ReceiveResultAsync(INavigationViewModel viewModel)
        {
            if (_tcs == null) return;

            try
            {
                var result = await _tcs.Task.ConfigureAwait(false);
                ResultReceived?.Invoke((TViewModel)viewModel, new NavigationResult<TResult>(true, result));
            }
            catch (Exception e)
            {
                ResultReceived?.Invoke((TViewModel)viewModel, new NavigationResult<TResult>(false, default(TResult), e));
            }
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
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
    }

    public class ReceivableNavigationPath<TViewModel, TParameter, TResult> : ReceivableNavigationPath<TViewModel, TResult> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; }

        public ReceivableNavigationPath(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate) : base(resultReceived, canNavigate)
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
