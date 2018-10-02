using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public static class NavigationUriFactory
    {
        public static NavigationUri Create(string path)
        {
            var navigation = new Navigation(path);
            return new NavigationUri(navigation);
        }

        public static NavigationUri<TViewModel> Create<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>(canNavigate);
            return new NavigationUri<TViewModel>(navigation);
        }

        public static NavigationUri<TViewModel> Create<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter, canNavigate);
            return new NavigationUri<TViewModel>(navigation);
        }

        public static NavigationUriResult<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationUriResult<TViewModel, TResult>(new NavigationUri<TViewModel>(navigation));
        }

        public static NavigationUriResult<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationUriResult<TViewModel, TResult>(new NavigationUri<TViewModel>(navigation));
        }

        public static NavigationUri CreateForTabbedPage(int selectedIndex = -1, params ITabNavigation[] tabNavigations)
        {
            var navigation = new TabbedNavigation(selectedIndex, tabNavigations);
            return new NavigationUri(navigation);
        }

        public static NavigationUri CreateForTabbedPage(string tabbedPageName, int selectedIndex = -1, params ITabNavigation[] tabNavigations)
        {
            var navigation = new TabbedNavigation(tabbedPageName, selectedIndex, tabNavigations);
            return new NavigationUri(navigation);
        }
    }
}
