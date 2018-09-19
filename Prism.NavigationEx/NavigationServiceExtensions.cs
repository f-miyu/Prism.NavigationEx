using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationServiceExtensions
    {
        public static Task NavigateAsync<TViewModel>(this INavigationService navigationService, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false) where TViewModel : NavigationViewModel
        {
            var name = NavigationNameProvider.GetName(typeof(TViewModel));

            if (wrapInNavigationPage)
            {
                name = NavigationNameProvider.NavigationPageName + "/" + name;
            }

            if (noHistory)
            {
                name = "/" + name;
            }

            return navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
        }

        public static Task NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false) where TViewModel : NavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationViewModel.ParameterKey,  parameter}
            };

            return navigationService.NavigateAsync<TViewModel>(parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public async static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null) where TViewModel : NavigationViewModel<TResult>
        {
            var tcs = new TaskCompletionSource<NavigationResult<TResult>>();

            var parameters = new NavigationParameters
            {
                {NavigationViewModel.TaskCompletionSourceKey, tcs}
            };

            using (cancellationToken?.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    await navigationService.NavigateAsync<TViewModel>(parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory).ConfigureAwait(false);

                    return await tcs.Task.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return new NavigationResult<TResult>(false, default(TResult), e);
                }
            }
        }

        public async static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null) where TViewModel : NavigationViewModelResult<TResult>
        {
            var tcs = new TaskCompletionSource<NavigationResult<TResult>>();

            var parameters = new NavigationParameters
            {
                {NavigationViewModel.ParameterKey,  parameter},
                {NavigationViewModel.TaskCompletionSourceKey, tcs}
            };

            using (cancellationToken?.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    await navigationService.NavigateAsync<TViewModel>(parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory).ConfigureAwait(false);

                    return await tcs.Task.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return new NavigationResult<TResult>(false, default(TResult), e);
                }
            }
        }

        public static Task GoBackWithResultAsync<TResult>(this INavigationService navigationService, TResult result, bool? useModalNavigation = null, bool animated = true)
        {
            var parameters = new NavigationParameters
            {
                {NavigationViewModel.ParameterKey, result}
            };

            return navigationService.GoBackAsync(parameters, useModalNavigation, animated);
        }

        public static Task GoBackToRootWithResultAsync<TResult>(this INavigationService navigationService, TResult result)
        {
            var parameters = new NavigationParameters
            {
                {NavigationViewModel.ParameterKey, result}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }
    }
}
