using System;
using Prism.Navigation;
namespace Prism.NavigationEx
{
    public interface INavigationResult<T>
    {
        bool Success { get; }
        T Data { get; }
        Exception Exception { get; }
    }
}
