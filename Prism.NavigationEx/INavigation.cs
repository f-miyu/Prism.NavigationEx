using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        (string Path, NavigationParameters Parameters) GetPathAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null);
    }

    public interface INavigation<TRootViewModel> : INavigation where TRootViewModel : INavigationViewModel
    {
    }
}
