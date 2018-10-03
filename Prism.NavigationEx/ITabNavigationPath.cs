using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface ITabNavigationPath
    {
        string Name { get; }
        string CreateTabParameter(NavigationParameters parameters);
    }
}
