using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface ITab
    {
        string GetPath(ref INavigationParameters parameters);
    }
}
