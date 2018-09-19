using System;
namespace Prism.NavigationEx
{
    public class NavigationResult<T> : INavigationResult<T>
    {
        public bool Success { get; }
        public T Data { get; }
        public Exception Exception { get; }

        public NavigationResult(bool success, T data, Exception exception = null)
        {
            Success = success;
            Data = data;
            Exception = exception;
        }
    }
}
