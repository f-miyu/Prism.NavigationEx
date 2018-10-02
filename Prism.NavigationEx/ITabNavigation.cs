using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface ITabNavigation
    {
        string Name { get; }
        string CreateTabParameter(NavigationParameters parameters);
    }
}
