using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public interface INavigationUri
    {
        (string Uri, NavigationParameters Parameters) GetUriAndParameters(NavigationParameters additionalParameters = null, NavigationParameters additionalQueries = null);
    }

    public interface INavigationUri<TRootViewModel> : INavigationUri where TRootViewModel : INavigationViewModel
    {
    }
}
