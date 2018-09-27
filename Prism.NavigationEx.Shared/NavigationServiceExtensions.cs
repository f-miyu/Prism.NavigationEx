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
        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationPath<TViewModel> navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync<TViewModel>(navigationPath, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationPath<TViewModel> navigationPath, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(navigationPath, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel>(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel, TParameter>(parameter), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel>(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel, TParameter>(parameter), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result, bool? useModalNavigation = null, bool animated = true)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsyncAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, INavigationPath<TViewModel> navigationPath, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(navigationPath, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsyncAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, INavigationPath<TViewModel> navigationPath, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(navigationPath, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel>(), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(NavigationPath.Create<TViewModel, TParameter>(parameter), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel>(), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(NavigationPath.Create<TViewModel, TParameter>(parameter), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModelResult<TResult, TConfirmParameter> viewModel, TResult result, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModelResult<TResult, TConfirmParameter> viewModel, TResult result, TConfirmParameter confirmParameter)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        private static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationPathBase<TViewModel> navigationPath, INavigationParameters additionalParameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            if (navigationPath == null)
                throw new ArgumentNullException(nameof(navigationPath));

            var (path, parameters) = navigationPath.GetPathAndParameters(additionalParameters);

            if (wrapInNavigationPage)
            {
                path = NavigationNameProvider.GetNavigationPageName(typeof(TViewModel)) + "/" + path;
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
