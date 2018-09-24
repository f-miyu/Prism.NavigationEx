using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationWithConfirmByViewModelExtensions
    {
        internal static async Task<bool> CanNavigateInternalAsync<TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, INavigationParameters parameters)
        {
            var result = true;

            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                result = await self.CanNavigateAtBackAsync(parameter).ConfigureAwait(false);
            }
            else
            {
                result = await self.CanNavigateAtNewAsync(parameter).ConfigureAwait(false);

                if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
                {
                    cts.Cancel();
                }
            }

            return result;
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(self, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(self, parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(self, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(self, parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(self, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(self, parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(self, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(self, parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackWithConfirmAsync(self, confirmParameter, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TConfirmParameter>(this INavigationWithConfirmByViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter)
        {
            return self.NavigationService.GoBackToRootWithConfirmAsync(self, confirmParameter);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TResult, TConfirmParameter>(this INavigationWithConfirmByViewModelResult<TResult, TConfirmParameter> self, TResult result, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackWithConfirmAsync(self, result, confirmParameter, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TResult, TConfirmParameter>(this INavigationWithConfirmByViewModelResult<TResult, TConfirmParameter> self, TResult result, TConfirmParameter confirmParameter)
        {
            return self.NavigationService.GoBackToRootWithConfirmAsync(self, result, confirmParameter);
        }
    }
}
