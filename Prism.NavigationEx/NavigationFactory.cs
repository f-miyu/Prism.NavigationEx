using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TViewModel, TResult>(TViewModel viewModel, INavigationResult<TResult> result);

    public static class NavigationFactory
    {
        public static Navigation Create(string path)
        {
            var navigationPath = new NavigationPath(path);
            return new Navigation(navigationPath);
        }

        public static Navigation<TViewModel> Create<TViewModel>(Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath<TViewModel>(canNavigate);
            return new Navigation<TViewModel>(navigationPath);
        }

        public static Navigation<TViewModel> Create<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath<TViewModel, TParameter>(parameter, canNavigate);
            return new Navigation<TViewModel>(navigationPath);
        }

        public static ResultNavigation<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel
        {
            var navigationPath = new ReceivableNavigationPath<TViewModel, TResult>(resultReceived, canNavigate);
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }

        public static ResultNavigation<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new ReceivableNavigationPath<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate);
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }

        public static Navigation CreateForTabbedPage(params ITab[] tabs)
        {
            var navigationPath = new TabbedNavigationPath(tabs);
            return new Navigation(navigationPath);
        }

        public static Navigation CreateForTabbedPage(string path, params ITab[] tabs)
        {
            var navigationPath = new TabbedNavigationPath(path, tabs);
            return new Navigation(navigationPath);
        }

        public static Navigation<TViewModel> CreateForTabbedPage<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new TabbedNavigationPath<TViewModel>(canNavigate, tabs);
            return new Navigation<TViewModel>(navigationPath);
        }

        public static Navigation<TViewModel> CreateForTabbedPage<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new TabbedNavigationPath<TViewModel, TParameter>(parameter, canNavigate, tabs);
            return new Navigation<TViewModel>(navigationPath);
        }

        public static ResultNavigation<TViewModel, TResult> CreateForTabbedPage<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new ReceivableTabbedNavigationPath<TViewModel, TResult>(resultReceived, canNavigate, tabs);
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }

        public static ResultNavigation<TViewModel, TResult> CreateForTabbedPage<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new ReceivableTabbedNavigationPath<TViewModel, TParameter, TResult>(parameter, resultReceived, canNavigate, tabs);
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }
    }
}