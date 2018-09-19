using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace Prism.NavigationEx
{
    public static class NavigationNameProvider
    {
        private static readonly Dictionary<Type, string> _names = new Dictionary<Type, string>();

        public static string NavigationPageName { get; set; } = nameof(NavigationPage);

        public static string GetName(Type viewModelType)
        {
            if (_names.ContainsKey(viewModelType))
            {
                return _names[viewModelType];
            }
            return Regex.Replace(viewModelType.Name, @"(.+)ViewModel$", "$1");
        }

        public static void Register(Type viewModelType, string name)
        {
            _names[viewModelType] = name;
        }
    }
}
