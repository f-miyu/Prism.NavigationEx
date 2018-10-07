using System;
using Prism.Navigation;
namespace Prism.NavigationEx
{
    public interface INavigationResult<T> : INavigationResult
    {
        T Data { get; }
    }
}
