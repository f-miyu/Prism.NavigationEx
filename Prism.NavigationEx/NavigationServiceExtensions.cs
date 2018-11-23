using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Linq;
using System.Collections.Generic;
using Prism.Common;
using Prism.Mvvm;

namespace Prism.NavigationEx
{
    public static class NavigationServiceExtensions
    {
        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, INavigation navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
        {
            if (navigation == null)
                throw new ArgumentNullException(nameof(navigation));

            var (path, parameters) = navigation.GetPathAndParameters();

            if (wrapInNavigationPage)
            {
                path = NavigationNameProvider.DefaultNavigationPageName + "/" + path;
            }

            if (replaced)
            {
                path = "../" + path;
            }

            if (noHistory)
            {
                path = "/" + path;
            }

            return navigationService.NavigateAsync(path, parameters, useModalNavigation, animated);
        }

        public static async Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigation<TViewModel> navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            if (navigation == null)
                throw new ArgumentNullException(nameof(navigation));

            var tcs = new TaskCompletionSource<TResult>();
            var cts = new CancellationTokenSource();

            using (cts.Token.Register(() => tcs.TrySetCanceled()))
            {
                using (cancellationToken?.Register(() => tcs.TrySetCanceled()))
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

                        var (path, parameters) = navigation.GetPathAndParameters(additionalParameters, additionalQueries);

                        if (wrapInNavigationPage)
                        {
                            path = NavigationNameProvider.DefaultNavigationPageName + "/" + path;
                        }

                        if (noHistory)
                        {
                            path = "/" + path;
                        }

                        var navigationResult = await navigationService.NavigateAsync(path, parameters, useModalNavigation, animated).ConfigureAwait(false);

                        if (!navigationResult.Success)
                        {
                            return new NavigationResult<TResult>(false, default(TResult), navigationResult.Exception);
                        }

                        var result = await tcs.Task.ConfigureAwait(false);
                        return new NavigationResult<TResult>(true, result);
                    }
                    catch (Exception e)
                    {
                        return new NavigationResult<TResult>(false, default(TResult), e);
                    }
                }
            }
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync(NavigationFactory.Create<TViewModel>(), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return navigationService.NavigateAsync(NavigationFactory.Create<TViewModel, TParameter>(parameter), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult> NavigateTabbedPageAsync(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, params ITab[] tabs)
        {
            return navigationService.NavigateAsync(NavigationFactory.Create(NavigationNameProvider.DefaultTabbedPageName, tabs), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }
    }
}
