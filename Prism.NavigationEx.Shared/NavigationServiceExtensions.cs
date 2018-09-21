using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationServiceExtensions
    {
        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), null, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), null, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync(firstNavigation, null, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync(firstNavigation, null, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, null, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, null, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
           where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), null, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), null, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmingAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync(firstNavigation, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateWithConfirmingAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync(firstNavigation, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmingAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmingAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> GoBackAsync<TResult>(this INavigationService navigationService, TResult result, string resultParameterKey, bool? useModalNavigation = null, bool animated = true)
        {
            var parameters = new NavigationParameters
            {
                {resultParameterKey, result}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync<TResult>(this INavigationService navigationService, TResult result, string resultParameterKey)
        {
            var parameters = new NavigationParameters
            {
                {resultParameterKey, result}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }

        private static Task<INavigationResult> NavigateWihTypeAsync(this INavigationService navigationService, Type viewModelType, NavigationParameters parameters, object confirmParameter = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            var name = NavigationNameProvider.GetNavigationName(viewModelType);

            if (wrapInNavigationPage)
            {
                name = NavigationNameProvider.GetNavigationPageName(viewModelType) + "/" + name;
            }

            if (noHistory)
            {
                name = "/" + name;
            }

            if (confirmParameter != null)
            {
                if (parameters == null)
                {
                    parameters = new NavigationParameters();
                }
                parameters.Add(NavigationParameterKey.ConfirmParameter, confirmParameter);
            }

            return navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
        }

        private async static Task<INavigationResult<TResult>> NavigateWihTypeAsync<TResult>(this INavigationService navigationService, Type viewModelType, NavigationParameters parameters, object confirmParameter = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }
            parameters.Add(NavigationParameterKey.TaskCompletionSource, tcs);

            var cts = new CancellationTokenSource();
            parameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

            if (confirmParameter != null)
            {
                parameters.Add(NavigationParameterKey.ConfirmParameter, confirmParameter);
            }

            var cancellationToken = cts.Token;

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    var result = await navigationService.NavigateWihTypeAsync(viewModelType, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory).ConfigureAwait(false);

                    if (!result.Success)
                    {
                        return new NavigationResult<TResult>(false, default(TResult), result.Exception);
                    }

                    return await tcs.Task.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return new NavigationResult<TResult>(false, default(TResult), e);
                }
            }
        }

        private static Task<INavigationResult> NavigateWithNavigationAsync(this INavigationService navigationService, INavigation firstNavigation, object confirmParameter = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
        {
            var parameters = new NavigationParameters();

            var path = NavigationNameProvider.GetNavigationName(firstNavigation.ViewModelType);
            if (firstNavigation.ParameterExists)
            {
                var id = Guid.NewGuid().ToString();
                path += $"?{NavigationParameterKey.ParameterId}={id}";
                parameters.Add(id, firstNavigation.Parameter);
            }

            if (navigations != null)
            {
                foreach (var navigation in navigations)
                {
                    path += $"/{NavigationNameProvider.GetNavigationName(navigation.ViewModelType)}";
                    if (navigation.ParameterExists)
                    {
                        var id = Guid.NewGuid().ToString();
                        path += $"?{NavigationParameterKey.ParameterId}={id}";
                        parameters.Add(id, navigation.Parameter);
                    }
                }
            }

            if (confirmParameter != null)
            {
                parameters.Add(NavigationParameterKey.ConfirmParameter, confirmParameter);
            }

            if (wrapInNavigationPage)
            {
                path = NavigationNameProvider.GetNavigationPageName(firstNavigation.ViewModelType) + "/" + path;
            }

            if (noHistory)
            {
                path = "/" + path;
            }

            return navigationService.NavigateAsync(path, parameters, useModalNavigation, animated);
        }

        private async static Task<INavigationResult<TResult>> NavigateWithNavigationAsync<TResult>(this INavigationService navigationService, INavigation firstNavigation, object confirmParameter = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    var parameters = new NavigationParameters();

                    var path = NavigationNameProvider.GetNavigationName(firstNavigation.ViewModelType);

                    var taskCompletionSourceId = Guid.NewGuid().ToString();
                    path += $"?{NavigationParameterKey.TaskCompletionSourceId}={taskCompletionSourceId}";
                    parameters.Add(taskCompletionSourceId, tcs);
                    parameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

                    if (firstNavigation.ParameterExists)
                    {
                        var id = Guid.NewGuid().ToString();
                        path += $"&{NavigationParameterKey.ParameterId}={id}";
                        parameters.Add(id, firstNavigation.Parameter);
                    }

                    if (navigations != null)
                    {
                        foreach (var navigation in navigations)
                        {
                            path += $"/{NavigationNameProvider.GetNavigationName(navigation.ViewModelType)}";
                            if (navigation.ParameterExists)
                            {
                                var id = Guid.NewGuid().ToString();
                                path += $"?{NavigationParameterKey.ParameterId}={id}";
                                parameters.Add(id, navigation.Parameter);
                            }
                        }
                    }

                    if (confirmParameter != null)
                    {
                        parameters.Add(NavigationParameterKey.ConfirmParameter, confirmParameter);
                    }

                    if (wrapInNavigationPage)
                    {
                        path = NavigationNameProvider.GetNavigationPageName(firstNavigation.ViewModelType) + "/" + path;
                    }

                    if (noHistory)
                    {
                        path = "/" + path;
                    }


                    var result = await navigationService.NavigateAsync(path, parameters, useModalNavigation, animated);

                    if (!result.Success)
                    {
                        return new NavigationResult<TResult>(false, default(TResult), result.Exception);
                    }

                    return await tcs.Task.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return new NavigationResult<TResult>(false, default(TResult), e);
                }
            }
        }
    }
}
