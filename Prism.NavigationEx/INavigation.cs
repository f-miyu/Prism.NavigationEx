using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        INavigation NextNavigation { get; set; }
        string CreateNavigationUri(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null);
    }
}
