using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigationPath
    {
        (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalPathParameters = null);
    }

    public interface INavigationPath<TRootViewModel> : INavigationPath where TRootViewModel : INavigationViewModel
    {
    }
}
