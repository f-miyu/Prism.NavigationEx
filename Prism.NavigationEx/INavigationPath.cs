using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigationPath
    {
        INavigationPath NextNavigationPath { get; set; }
        string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null);
    }
}
