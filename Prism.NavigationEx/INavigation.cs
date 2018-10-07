using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        (string Path, INavigationParameters Parameters) GetPathAndParameters(INavigationParameters additionalParameters = null, INavigationParameters additionalQueries = null);
    }

    public interface INavigation<TRootViewModel> : INavigation where TRootViewModel : INavigationViewModel
    {
    }
}
