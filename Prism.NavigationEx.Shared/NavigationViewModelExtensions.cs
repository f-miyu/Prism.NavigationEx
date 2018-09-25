using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationViewModelExtensions
    {
        internal static void PrepareIfNeeded<TParameter>(this INavigationViewModel<TParameter> self, INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.TryGetValue<TParameter>(NavigationParameterKey.Parameter, out var parameter))
                {
                    self.Prepare(parameter);
                }
                else if (parameters.TryGetValue<string>(NavigationParameterKey.ParameterId, out var id))
                {
                    if (parameters.TryGetValue<TParameter>(id, out parameter))
                    {
                        self.Prepare(parameter);
                    }
                }
            }
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationViewModel self, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return self.NavigationService.NavigateAsync<TViewModel>(self, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationViewModel self, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TParameter>(self, parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationViewModel self, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TResult>(self, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationViewModel self, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TParameter, TResult>(self, parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationViewModel self, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return self.NavigationService.NavigateAsync<TViewModel>(self, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationViewModel self, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TParameter>(self, parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationViewModel self, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TResult>(self, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationViewModel self, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return self.NavigationService.NavigateAsync<TViewModel, TParameter, TResult>(self, parameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> GoBackAsync(this INavigationViewModel self, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackAsync(self, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync(this INavigationViewModel self)
        {
            return self.NavigationService.GoBackToRootAsync(self);
        }

        public static Task<INavigationResult> GoBackAsync<TResult>(this INavigationViewModelResult<TResult> self, TResult result, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackAsync(self, result, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync<TResult>(this INavigationViewModelResult<TResult> self, TResult result)
        {
            return self.NavigationService.GoBackToRootAsync(self, result);
        }
    }
}
