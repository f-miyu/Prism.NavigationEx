using System;
using System.Collections.Generic;
using Prism.Navigation;
using System.Linq;
using Xamarin.Forms;

namespace Prism.NavigationEx
{
    public class TabbedNavigation : INavigation
    {
        public INavigation NextNavigation { get; set; }
        public TabNavigation[] TabNavigations { get; }
        public int SelectedIndex { get; }
        public string TabbedPageName { get; }

        public TabbedNavigation(int selectedIndex = -1, params TabNavigation[] tabNavigations) : this(nameof(TabbedPage), selectedIndex, tabNavigations)
        {
        }

        public TabbedNavigation(string tabbedPageName, int selectedIndex = -1, params TabNavigation[] tabNavigations)
        {
            if (tabNavigations != null)
            {
                if (tabNavigations.Length != tabNavigations.Select(t => t.Name).Distinct().Count())
                {
                    throw new ArgumentException("duplicate name", nameof(tabNavigations));
                }

                if (selectedIndex >= tabNavigations.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(selectedIndex));
                }
            }

            TabbedPageName = tabbedPageName;
            SelectedIndex = selectedIndex;
            TabNavigations = tabNavigations;
        }

        public string CreateNavigationPath(NavigationParameters parameters, NavigationParameters pathParameters = null, NavigationParameters nextPathParameters = null)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            if (pathParameters == null)
            {
                pathParameters = new NavigationParameters();
            }

            if (TabNavigations != null && TabNavigations.Length > 0)
            {
                foreach (var tabNavigations in TabNavigations)
                {
                    pathParameters.Add(KnownNavigationParameters.CreateTab, tabNavigations.CreateTabParameter(parameters));
                }

                if (SelectedIndex >= 0)
                {
                    pathParameters.Add(KnownNavigationParameters.SelectedTab, TabNavigations[SelectedIndex].Name);
                }
            }

            var path = TabbedPageName;

            if (pathParameters.Count > 0)
            {
                path += "?" + string.Join("&", pathParameters.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigation != null)
            {
                path += "/" + NextNavigation.CreateNavigationPath(parameters, nextPathParameters);
            }

            return path;
        }
    }

    public class TabbedNavigation<TViewModel> : TabbedNavigation where TViewModel : INavigationViewModel
    {
        public TabbedNavigation(int selectedIndex = -1, params TabNavigation[] tabNavigations) : base(NavigationNameProvider.GetNavigationName(typeof(TViewModel)), selectedIndex, tabNavigations)
        {
        }
    }
}
