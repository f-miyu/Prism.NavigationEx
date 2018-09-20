using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace Prism.NavigationEx
{
    public static class NavigationNameProvider
    {
        private static Func<Type, string> _defaultViewModelTypeToNavigationNameResolver =
            viewModelType => Regex.Replace(viewModelType.Name, @"(.+)ViewModel$", "$1");

        private static Func<Type, string> _defaultViewModelTypeToNavigationPageNameResolver =
            viewModelType => "NavigationPage";

        public static string GetNavigationName(Type viewModelType)
        {
            return _defaultViewModelTypeToNavigationNameResolver(viewModelType);
        }

        public static string GetNavigationPageName(Type viewModelType)
        {
            return _defaultViewModelTypeToNavigationPageNameResolver(viewModelType);
        }

        public static void SetDefaultViewModelTypeToNavigationNameResolver(Func<Type, string> viewModelTypeToNavigationNameResolver)
        {
            _defaultViewModelTypeToNavigationNameResolver = viewModelTypeToNavigationNameResolver;
        }

        public static void SetDefaultViewModelTypeToNavigationPageNameResolver(Func<Type, string> viewModelTypeToNavigationPageNameResolver)
        {
            _defaultViewModelTypeToNavigationPageNameResolver = viewModelTypeToNavigationPageNameResolver;
        }
    }
}
