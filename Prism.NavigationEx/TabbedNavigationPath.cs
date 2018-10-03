using System;
using System.Collections.Generic;
using Prism.Navigation;
using System.Linq;
using Xamarin.Forms;

namespace Prism.NavigationEx
{
    public class TabbedNavigationPath : INavigationPath
    {
        public INavigationPath NextNavigationPath { get; set; }
        public ITabNavigationPath[] TabNavigations { get; }
        public int SelectedIndex { get; }
        public string TabbedPageName { get; }

        public TabbedNavigationPath(int selectedIndex = -1, params ITabNavigationPath[] tabNavigations) : this(nameof(TabbedPage), selectedIndex, tabNavigations)
        {
        }

        public TabbedNavigationPath(string tabbedPageName, int selectedIndex = -1, params ITabNavigationPath[] tabNavigations)
        {
            if (tabNavigations != null)
            {
                //if (tabNavigations.Length != tabNavigations.Select(t => t.Name).Distinct(.Count())
                //{
                //    throw new ArgumentException("duplicate name", nameof(tabNavigations));
                //}

                if (selectedIndex >= tabNavigations.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(selectedIndex));
                }
            }

            TabbedPageName = tabbedPageName;
            SelectedIndex = selectedIndex;
            TabNavigations = tabNavigations;
        }

        public string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (queries == null)
            {
                queries = new NavigationParameters();
            }

            if (TabNavigations != null && TabNavigations.Length > 0)
            {
                foreach (var tabNavigations in TabNavigations)
                {
                    queries.Add(KnownNavigationParameters.CreateTab, tabNavigations.CreateTabParameter(parameters));
                }

                if (SelectedIndex >= 0)
                {
                    queries.Add(KnownNavigationParameters.SelectedTab, TabNavigations[SelectedIndex].Name);
                }
            }

            var path = TabbedPageName;

            if (queries.Count > 0)
            {
                path += "?" + string.Join("&", queries.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigationPath != null)
            {
                path += "/" + NextNavigationPath.GetPath(parameters, nextQueries);
            }

            return path;
        }
    }

    public class TabbedNavigationPath<TViewModel> : TabbedNavigationPath where TViewModel : INavigationViewModel
    {
        public TabbedNavigationPath(int selectedIndex = -1, params ITabNavigationPath[] tabNavigations) : base(NavigationNameProvider.GetNavigationName(typeof(TViewModel)), selectedIndex, tabNavigations)
        {
        }
    }
}
