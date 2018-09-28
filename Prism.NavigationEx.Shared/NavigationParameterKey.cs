using System;
namespace Prism.NavigationEx
{
    public static class NavigationParameterKey
    {
        public static readonly string ParameterId = "parameterId";
        public static readonly string TaskCompletionSourceId = "taskCompletionSourceId";
        public static readonly string ConfirmParameter = "confirmParameter";
        public static readonly string CancellationTokenSource = "cancellationTokenSource";
        public static readonly string Result = "result";
        public static readonly string ReceiveResultId = "receiveResultId";
        public static readonly string CanNavigateId = "canNavigateId";
        public static readonly string CanNavigate = "canNavigate";
    }
}
