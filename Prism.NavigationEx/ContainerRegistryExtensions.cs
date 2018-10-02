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
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var viewTypes = assembly.DefinedTypes.Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Page))).Select(t => t.AsType());

            foreach (var viewType in viewTypes)
            {
                var name = viewType.Name;
                containerRegistry.RegisterForNavigation(viewType, name);
            }

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TabbedPage>();
        }

        public static void RegisterForNavigation(this IContainerRegistry containerRegistry, Application application)
        {
            if (application == null)
                throw new ArgumentNullException(nameof(application));

            var assembly = application.GetType().GetTypeInfo().Assembly;
            containerRegistry.RegisterForNavigation(assembly);
        }
    }
}