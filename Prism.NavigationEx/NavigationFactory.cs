using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public delegate void ResultReceivedDelegate<TResult>(INavigationViewModel viewModel, INavigationResult<TResult> result);

    public static class NavigationFactory
    {
        public static Navigation Create(string path, params ITab[] tabs)
        {
            var navigationPath = new NavigationPath
            {
                Path = path,
                Tabs = tabs
            };
            return new Navigation(navigationPath);
        }

        public static Navigation<TViewModel> Create<TViewModel>(Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath
            {
                ViewModelType = typeof(TViewModel),
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            return new Navigation<TViewModel>(navigationPath);
        }

        public static Navigation<TViewModel> Create<TViewModel, TParameter>(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath
            {
                ViewModelType = typeof(TViewModel),
                Parameter = parameter,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            return new Navigation<TViewModel>(navigationPath);
        }

        public static ResultNavigation<TViewModel, TResult> Create<TViewModel, TResult>(ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel
        {
            var navigationPath = new NavigationPath<TResult>
            {
                ViewModelType = typeof(TViewModel),
                ResultReceived = resultReceived,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }

        public static ResultNavigation<TViewModel, TResult> Create<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigationPath = new NavigationPath<TResult>
            {
                ViewModelType = typeof(TViewModel),
                Parameter = parameter,
                ResultReceived = resultReceived,
                CanNavigate = canNavigate,
                Tabs = tabs
            };
            return new ResultNavigation<TViewModel, TResult>(new Navigation<TViewModel>(navigationPath));
        }
    }
}