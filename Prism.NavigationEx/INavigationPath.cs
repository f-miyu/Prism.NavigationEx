using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigationPath
    {
        INavigationPath NextNavigationPath { get; set; }
        string GetPath(INavigationParameters parameters, INavigationParameters queries = null, INavigationParameters nextQueries = null);
    }
}
