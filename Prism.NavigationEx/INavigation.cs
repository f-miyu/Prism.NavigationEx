using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        INavigation NextNavigation { get; set; }
        string CreateNavigationPath(NavigationParameters parameters, NavigationParameters pathParameters = null, NavigationParameters nextPathParameters = null);
    }
}
