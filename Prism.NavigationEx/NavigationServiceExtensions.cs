using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Linq;
using System.Collections.Generic;

namespace Prism.NavigationEx
{
    public static class NavigationServiceExtensions
    {
        public static Task NavigateAsync(this INavigationService navigationService, INavigationUri navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
        {
            if (navigationPath == null)
                throw new ArgumentNullException(nameof(navigationPath));

            var (uri, parameters) = navigationPath.GetUriAndParameters();

            if (wrapInNavigationPage)
            {
                uri = NavigationNameProvider.DefaultNavigationPageName + "/" + uri;
            }

            if (replaced)
            {
                uri = "../" + uri;
            }

            if (noHistory)
            {
                uri = "/" + uri;
            }

            return navigationService.NavigateAsync(uri, parameters, useModalNavigation, animated);
        }

        public static async Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationUri<TViewModel> navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            if (navigationPath == null)
                throw new ArgumentNullException(nameof(navigationPath));

            var tcs = new TaskCompletionSource<TResult>();
            var cts = new CancellationTokenSource();

            var cancellationToken = cts.Token;

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    var taskCompletionSourceId = Guid.NewGuid().ToString();
                    var additionalQueries = new NavigationParameters
                    {
                        {NavigationParameterKey.TaskCompletionSourceId, taskCompletionSourceId}
                    };

                    var additionalParameters = new NavigationParameters();
                    additionalParameters.Add(taskCompletionSourceId, tcs);
                    additionalParameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

                    var (uri, parameters) = navigationPath.GetUriAndParameters(additionalParameters, additionalQueries);

                    if (wrapInNavigationPage)
                    {
                        uri = NavigationNameProvider.DefaultNavigationPageName + "/" + uri;
                    }

                    if (noHistory)
                    {
                        uri = "/" + uri;
                    }

                    await navigationService.NavigateAsync(uri, parameters, useModalNavigation, animated).ConfigureAwait(false);

                    var result = await tcs.Task.ConfigureAwait(false);
                    return new NavigationResult<TResult>(true, result);
                }
                catch (Exception e)
                {
                    return new NavigationResult<TResult>(false, default(TResult), e);
                }
            }
        }

        public static Task NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync(NavigationUriFactory.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return navigationService.NavigateAsync(NavigationUriFactory.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationUriFactory.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationUriFactory.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<bool> GoBackAsync(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<bool> GoBackAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result, bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task GoBackToRootAsync(this INavigationService navigationService, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }
    }
}
