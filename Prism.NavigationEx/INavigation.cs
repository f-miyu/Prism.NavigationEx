using System;
using System.Collections.Generic;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        INavigation NextNavigation { get; set; }
        string CreateNavigationPath(INavigationParameters parameters, IDictionary<string, string> pathParameters = null, IDictionary<string, string> nextPathParameters = null);
    }
}
