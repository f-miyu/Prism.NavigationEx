using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace Prism.NavigationEx
{
    public static class NavigationNameProvider
    {
        private static Func<Type, string> _defaultViewModelTypeToNavigationNameResolver =
            viewModelType =>
            {
                var suffix = "ViewModel";
                var name = viewModelType.Name;
                if (name.EndsWith(suffix, StringComparison.Ordinal))
                {
                    name = name.Substring(0, name.Length - suffix.Length);
                }
                return name;
            };

        public static string DefaultNavigationPageName { get; set; } = nameof(NavigationPage);

        public static string DefaultTabbedPageName { get; set; } = nameof(TabbedPage);

        public static string GetNavigationName(Type viewModelType)
        {
            return _defaultViewModelTypeToNavigationNameResolver(viewModelType);
        }

        public static void SetDefaultViewModelTypeToNavigationNameResolver(Func<Type, string> viewModelTypeToNavigationNameResolver)
        {
            _defaultViewModelTypeToNavigationNameResolver = viewModelTypeToNavigationNameResolver;
        }
    }
}
