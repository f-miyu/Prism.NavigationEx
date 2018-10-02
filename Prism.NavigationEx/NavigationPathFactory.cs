using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public static class NavigationPathFactory
    {
        public static NavigationPath Create(string path)
        {
            var navigation = new Navigation(path);
            return new NavigationPath(navigation);
        }

        public static NavigationPath<TViewModel> Create<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>(canNavigate);
            return new NavigationPath<TViewModel>(navigation);
        }

        public static NavigationPath<TViewModel> Create<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter, canNavigate);
            return new NavigationPath<TViewModel>(navigation);
        }

        public static NavigationPathResult<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived, canNavigate);
            return new NavigationPathResult<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }

        public static NavigationPathResult<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new NavigationPathResult<TViewModel, TResult>(new NavigationPath<TViewModel>(navigation));
        }

        public static NavigationPath CreateForTabbedPage(int selectedIndex = -1, params ITabNavigation[] tabNavigations)
        {
            var navigation = new TabbedNavigation(selectedIndex, tabNavigations);
            return new NavigationPath(navigation);
        }

        public static NavigationPath CreateForTabbedPage(string tabbedPageName, int selectedIndex = -1, params ITabNavigation[] tabNavigations)
        {
            var navigation = new TabbedNavigation(tabbedPageName, selectedIndex, tabNavigations);
            return new NavigationPath(navigation);
        }
    }
}
