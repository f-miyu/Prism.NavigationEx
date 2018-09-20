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
            return navigationService.NavigateAsync<TViewModel>(null, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter}
            };

            return navigationService.NavigateAsync<TViewModel>(parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.TaskCompletionSource, tcs}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(parameters, tcs, useModalNavigation, animated, wrapInNavigationPage, noHistory, cancellationToken);
        }

        public static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var tcs = new TaskCompletionSource<INavigationResult<TResult>>();

            var parameters = new NavigationParameters
            {
                {NavigationParameterKey.Parameter,  parameter},
                {NavigationParameterKey.TaskCompletionSource, tcs}
            };

            return navigationService.NavigateAsync<TViewModel, TResult>(parameters, tcs, useModalNavigation, animated, wrapInNavigationPage, noHistory, cancellationToken);
        }

        private static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            var viewModelType = typeof(TViewModel);
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

        private async static Task<INavigationResult<TResult>> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, NavigationParameters parameters, TaskCompletionSource<INavigationResult<TResult>> tcs, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, CancellationToken? cancellationToken = null)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            using (cancellationToken?.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    var result = await navigationService.NavigateAsync<TViewModel>(parameters, useModalNavigation, animated, wrapInNavigationPage, noHistory).ConfigureAwait(false);

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

        public static Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel
        {
            var firstNavigation = new Navigation<TViewModel>();
            return navigationService.NavigateAsync(firstNavigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter> { Parameter = parameter };
            return navigationService.NavigateAsync(firstNavigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TResult>(this INavigationService navigationService, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var firstNavigation = new NavigationResult<TViewModel, TResult>();
            return navigationService.NavigateAsync(firstNavigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        public static Task<INavigationResult> NavigateAsync<TViewModel, TParameter, TResult>(this INavigationService navigationService, TParameter parameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false, params INavigation[] navigations)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            var firstNavigation = new Navigation<TViewModel, TParameter, TResult> { Parameter = parameter };
            return navigationService.NavigateAsync(firstNavigation, useModalNavigation, animated, wrapInNavigationPage, noHistory, navigations);
        }

        private static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, INavigation firstNavigation, bool? useModalNavigation, bool animated, bool wrapInNavigationPage, bool noHistory, params INavigation[] navigations)
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

        public static Task<INavigationResult> GoBackAsync<TViewModel, TResult>(this INavigationService navigationService, TViewModel viewModel, TResult result, bool? useModalNavigation = null, bool animated = true)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {viewModel.ResultParameterKey, result}
            };

            return navigationService.GoBackAsync(parameters: parameters, useModalNavigation: useModalNavigation, animated: animated);
        }

        public static Task<INavigationResult> GoBackToRootAsync<TViewModel, TResult>(this INavigationService navigationService, TViewModel viewModel, TResult result)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            var parameters = new NavigationParameters
            {
                {viewModel.ResultParameterKey, result}
            };

            return navigationService.GoBackToRootAsync(parameters);
        }
    }
}
