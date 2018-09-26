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
        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, INavigation navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            return navigationService.NavigateAsync(navigation, new NavigationParameters(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigation<TViewModel> navigation, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(navigation, new NavigationParameters(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateAsync(new Navigation<TViewModel>(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return navigationService.NavigateAsync(new Navigation<TViewModel, TParameter>(parameter), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(new Navigation<TViewModel>(), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return navigationService.NavigateAsync<TViewModel, TResult>(new Navigation<TViewModel, TParameter>(parameter), useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackAsync(this INavigationService navigationService, INavigationViewModel viewModel, bool? useModalNavigation = null, bool animated = true)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync(this INavigationService navigationService, INavigationViewModel viewModel)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        public static Task<INavigationResult> GoBackAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result, bool? useModalNavigation = null, bool animated = true)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync<TResult>(this INavigationService navigationService, INavigationViewModelResult<TResult> viewModel, TResult result)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        internal static Task<INavigationResult> NavigateWithConfirmAsyncAsync<TConfirmParameter>(this INavigationService navigationService, INavigation navigation, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(navigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        internal static Task<INavigationResult<TResult>> NavigateWithConfirmAsyncAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, INavigation<TViewModel> navigation, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(navigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        internal static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(new Navigation<TViewModel>(), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        internal static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync(new Navigation<TViewModel, TParameter>(parameter), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        internal static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(new Navigation<TViewModel>(), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        internal static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(new Navigation<TViewModel, TParameter>(parameter), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModelResult<TResult, TConfirmParameter> viewModel, TResult result, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModelResult<TResult, TConfirmParameter> viewModel, TResult result, TConfirmParameter confirmParameter)
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Result, result},
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        private static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, INavigation navigation, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            var path = navigation.CreateNavigationPath(parameters);

            if (wrapInNavigationPage)
            {
                path = NavigationNameProvider.GetNavigationPageName(navigation.ViewModelType) + "/" + path;
            }

            if (noHistory)
            {
                path = "/" + path;
            }

            return navigationService.NavigateAsync(path, parameters, useModalNavigation, animated);
        }

        private static async Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigation<TViewModel> navigation, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var tcs = new TaskCompletionSource<TResult>();
            var cts = new CancellationTokenSource();

            var cancellationToken = cts.Token;

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    if (parameters == null)
                    {
                        parameters = new NavigationParameters();
                    }

                    var taskCompletionSourceId = Guid.NewGuid().ToString();
                    var pathParameters = new Dictionary<string, string>
                    {
                        [NavigationParameterKey.TaskCompletionSourceId] = taskCompletionSourceId
                    };
                    parameters.Add(taskCompletionSourceId, tcs);
                    parameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

                    var path = navigation.CreateNavigationPath(parameters, pathParameters);

                    if (wrapInNavigationPage)
                    {
                        path = NavigationNameProvider.GetNavigationPageName(navigation.ViewModelType) + "/" + path;
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
