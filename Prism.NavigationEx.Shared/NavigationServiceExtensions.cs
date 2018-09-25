using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Linq;

namespace Prism.NavigationEx
{
    public static class NavigationServiceExtensions
    {
        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter}
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
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

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationViewModel viewModel, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, INavigationViewModel viewModel, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationViewModel viewModel, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, INavigationViewModel viewModel, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, INavigationViewModel viewModel, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, INavigationViewModel viewModel, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, INavigationViewModel viewModel, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, INavigationViewModel viewModel, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.OnNavigatingFrom,  onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
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

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            return navigationService.NavigateWihTypeAsync<TResult>(typeof(TViewModel), parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this INavigationService navigationService, IConfirmNavigationViewModel<TConfirmParameter> viewModel, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            Action<INavigationParameters> onNavigatingFrom = viewModel.OnNavigatingFrom;
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.ConfirmParameter, confirmParameter},
                {NavigationParameterKey.OnNavigatingFrom, onNavigatingFrom}
            };

            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateWithNavigationAsync<TResult>(firstNavigation, parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
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

        private static Task<INavigationResult> NavigateWihTypeAsync(this INavigationService navigationService, Type viewModelType, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
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

            return navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
        }

        private async static Task<INavigationResult<TResult>> NavigateWihTypeAsync<TResult>(this INavigationService navigationService, Type viewModelType, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }
            parameters.Add(NavigationParameterKey.TaskCompletionSource, tcs);

            var cts = new CancellationTokenSource();
            parameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

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

        private static Task<INavigationResult> NavigateWithNavigationAsync(this INavigationService navigationService, INavigation firstNavigation, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            var path = CreatePath(firstNavigation, parameters);

            if (navigations != null)
            {
                foreach (var navigation in navigations)
                {
                    path += $"/{CreatePath(navigation, parameters)}";
                }
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

        private async static Task<INavigationResult<TResult>> NavigateWithNavigationAsync<TResult>(this INavigationService navigationService, INavigation firstNavigation, INavigationParameters parameters, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

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

                    var path = CreatePath(firstNavigation, parameters);

                    var taskCompletionSourceId = Guid.NewGuid().ToString();
                    path += $"&{NavigationParameterKey.TaskCompletionSourceId}={taskCompletionSourceId}";
                    parameters.Add(taskCompletionSourceId, tcs);
                    parameters.Add(NavigationParameterKey.CancellationTokenSource, cts);

                    if (navigations != null)
                    {
                        foreach (var navigation in navigations)
                        {
                            path += $"/{CreatePath(navigation, parameters)}?";
                        }
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

        private static string CreatePath(INavigation navigation, INavigationParameters parameters)
        {
            var path = $"{NavigationNameProvider.GetNavigationName(navigation.ViewModelType)}?";

            if (navigation.ParameterExists)
            {
                var parameterId = Guid.NewGuid().ToString();
                path += $"{NavigationParameterKey.ParameterId}={parameterId}&";
                parameters.Add(parameterId, navigation.Parameter);
            }

            Action<INavigationViewModel, INavigationParameters> onResult = navigation.OnResult;
            var onResultId = Guid.NewGuid().ToString();
            path += $"{NavigationParameterKey.OnResultId}={onResultId}";
            parameters.Add(onResultId, onResult);

            return path;
        }
    }
}
