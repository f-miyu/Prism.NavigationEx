using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public class ReceivableNavigation<TViewModel, TResult> : Navigation<TViewModel> where TViewModel : INavigationViewModel
    {
        protected ResultReceivedDelegate<TViewModel, TResult> _resultReceived;
        protected TaskCompletionSource<TResult> _tcs;

        public ReceivableNavigation(ResultReceivedDelegate<TViewModel, TResult> resultReceived)
        {
            _resultReceived = resultReceived;
        }

        public async Task ReceiveResultAsync(INavigationViewModel viewModel)
        {
            if (_tcs == null) return;

            try
            {
                var result = await _tcs.Task.ConfigureAwait(false);
                _resultReceived?.Invoke((TViewModel)viewModel, new NavigationResult<TResult>(true, result));
            }
            catch (Exception e)
            {
                _resultReceived?.Invoke((TViewModel)viewModel, new NavigationResult<TResult>(false, default(TResult), e));
            }
        }

        public override string CreateNavigationPath(INavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (NextNavigation != null && _resultReceived != null)
            {
                if (pathParameters == null)
                {
                    pathParameters = new Dictionary<string, string>();
                }

                if (nextPathParameters == null)
                {
                    nextPathParameters = new Dictionary<string, string>();
                }

                Func<INavigationViewModel, Task> receiveResult = ReceiveResultAsync;
                var receiveResultId = Guid.NewGuid().ToString();
                pathParameters[NavigationParameterKey.ReceiveResultId] = receiveResultId;
                parameters.Add(receiveResultId, receiveResult);

                if (_tcs != null && !_tcs.Task.IsCompleted)
                {
                    _tcs.TrySetCanceled();
                }

                _tcs = new TaskCompletionSource<TResult>();
                var taskCompletionSourceId = Guid.NewGuid().ToString();
                nextPathParameters[NavigationParameterKey.TaskCompletionSourceId] = taskCompletionSourceId;
                parameters.Add(taskCompletionSourceId, _tcs);
            }

            return base.CreateNavigationPath(parameters, pathParameters, nextPathParameters);
        }
    }

    public class ReceivableNavigation<TViewModel, TParameter, TResult> : ReceivableNavigation<TViewModel, TResult> where TViewModel : INavigationViewModel<TParameter>
    {
        public TParameter Parameter { get; private set; }

        public ReceivableNavigation(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) : base(resultReceived)
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
