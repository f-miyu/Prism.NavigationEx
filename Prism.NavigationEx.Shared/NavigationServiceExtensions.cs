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
        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationPath<TViewModel> navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync<TViewModel>(navigationPath, null, useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationPath<TViewModel> navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(navigationPath, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory, replaced);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel>(canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, Func<Task<bool>> canNavigate = null)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel, TParameter>(parameter, canNavigate), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackAsync(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result, bool? useModalNavigation = null, bool animated = true, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync(this INavigationService navigationService, Func<Task<bool>> canNavigate = null)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.CanNavigate, canNavigate}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        private static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationPathBase<TViewModel> navigationPath, INavigationParameters additionalParameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, bool replaced = false)
            where TViewModel : INavigationViewModel
        {
            if (navigationPath == null)
                throw new ArgumentNullException(nameof(navigationPath));

            var (path, parameters) = navigationPath.GetPathAndParameters(additionalParameters);

            if (wrapInNavigationPage)
            {
                path = NavigationNameProvider.GetNavigationPageName(typeof(TViewModel)) + "/" + path;
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

        private static async Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationPathBase<TViewModel> navigationPath, INavigationParameters additionalParameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
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
                    var additionalPathParameters = new Dictionary<string, string>
                    {
                        [NavigationParameterKey.TaskCompletionSourceId] = taskCompletionSourceId
                    };

                    if (additionalParameters == null)
                    {
                        additionalParameters = new NavigationParameters();
                    }
                    additionalParameters.Add(taskCompletionSourceId, tcs);
                    additionalParameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

                    var (path, parameters) = navigationPath.GetPathAndParameters(additionalParameters, additionalPathParameters);

                    if (wrapInNavigationPage)
                    {
                        path = NavigationNameProvider.GetNavigationPageName(typeof(TViewModel)) + "/" + path;
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
}
