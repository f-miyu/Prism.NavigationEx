using System;
using Prism.Ioc;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Prism.Mvvm;

namespace Prism.NavigationEx
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterForNavigation<TView, TViewModel>(this IContainerRegistry containerRegistry, string name = null)
            where TView : Page
            where TViewModel : NavigationViewModel
        {
            var viewType = typeof(TView);
            var viewModelType = typeof(TViewModel);

            containerRegistry.RegisterForNavigationWithViewModelType(viewType, viewModelType, name);
        }

        public static void RegisterForNavigationWithViewModelType(this IContainerRegistry containerRegistry, Type viewType, Type viewModelType, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = viewType.Name;
            }

            NavigationNameProvider.Register(viewModelType, name);

            ViewModelLocationProvider.Register(viewType.ToString(), viewModelType);

            containerRegistry.RegisterForNavigation(viewType, name);
        }

        public static void RegisterForNavigationOnPlatform<TView, TViewModel>(this IContainerRegistry containerRegistry, string name, params IPlatform[] platforms)
            where TView : Page
            where TViewModel : NavigationViewModel
        {
            var viewType = typeof(TView);
            var viewModelType = typeof(TViewModel);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = viewType.Name;
            }

            foreach (var platform in platforms)
            {
                if (Device.RuntimePlatform == platform.RuntimePlatform.ToString())
                {
                    containerRegistry.RegisterForNavigationWithViewModelType(platform.ViewType, viewModelType, name);
                }
            }

            containerRegistry.RegisterForNavigation<TView, TViewModel>(name);
        }

        public static void RegisterForNavigationOnIdiom<TView, TViewModel>(this IContainerRegistry containerRegistry, string name = null, Type desktopView = null, Type tabletView = null, Type phoneView = null)
            where TView : Page
            where TViewModel : NavigationViewModel
        {
            var viewType = typeof(TView);
            var viewModelType = typeof(TViewModel);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = viewType.Name;
            }

            if (Device.Idiom == TargetIdiom.Desktop && desktopView != null)
            {
                containerRegistry.RegisterForNavigationWithViewModelType(desktopView, viewType, name);
            }
            else if (Device.Idiom == TargetIdiom.Phone && phoneView != null)
            {
                containerRegistry.RegisterForNavigationWithViewModelType(phoneView, viewType, name);
            }
            else if (Device.Idiom == TargetIdiom.Tablet && tabletView != null)
            {
                containerRegistry.RegisterForNavigationWithViewModelType(tabletView, viewType, name);
            }
            else
            {
                containerRegistry.RegisterForNavigation<TView, TViewModel>(name);
            }
        }

        public static void RegisterForNavigations(this IContainerRegistry containerRegistry, Assembly assembly, IDictionary<Type, string> names = null, IDictionary<Type, Type> viewModelTypes = null)
        {
            var types = assembly.GetTypes();

            if (types == null)
                return;

            var viewTypes = types.Where(t => t.IsSubclassOf(typeof(Page)));

            foreach (var viewType in viewTypes)
            {
                var name = viewType.Name;
                if (names != null && names.ContainsKey(viewType))
                {
                    name = names[viewType];
                }

                if (viewModelTypes != null && viewModelTypes.ContainsKey(viewType))
                {
                    var viewModelType = viewModelTypes[viewType];

                    containerRegistry.RegisterForNavigationWithViewModelType(viewType, viewModelType, name);
                }
                else
                {
                    containerRegistry.RegisterForNavigation(viewType, name);
                }
            }
        }
    }
}