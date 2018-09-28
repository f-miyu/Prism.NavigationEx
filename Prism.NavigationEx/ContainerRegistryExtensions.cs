using System;
using Prism.Ioc;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;

namespace Prism.NavigationEx
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterForNavigation(this IContainerRegistry containerRegistry, Assembly assembly)
        {
            var types = assembly.GetTypes();

            if (types == null)
                return;

            var viewTypes = types.Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Page)));

            foreach (var viewType in viewTypes)
            {
                var name = viewType.Name;
                containerRegistry.RegisterForNavigation(viewType, name);
            }
        }

        public static void RegisterForNavigation<TNavigationPage>(this IContainerRegistry containerRegistry, Assembly assembly)
            where TNavigationPage : NavigationPage
        {
            containerRegistry.RegisterForNavigation(assembly);

            var navigationPageType = typeof(TNavigationPage);
            var navigationPageName = navigationPageType.Name;

            containerRegistry.RegisterForNavigation(navigationPageType, navigationPageName);

            NavigationNameProvider.SetDefaultViewModelTypeToNavigationPageNameResolver(_ => navigationPageName);
        }

        public static void RegisterForNavigation(this IContainerRegistry containerRegistry, Application application)
        {
            var assembly = application.GetType().Assembly;
            containerRegistry.RegisterForNavigation(assembly);
        }

        public static void RegisterForNavigation<TNavigationPage>(this IContainerRegistry containerRegistry, Application application)
            where TNavigationPage : NavigationPage
        {
            var assembly = application.GetType().Assembly;
            containerRegistry.RegisterForNavigation<TNavigationPage>(assembly);
        }
    }
}