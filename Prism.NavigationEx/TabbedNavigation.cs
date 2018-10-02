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
        public ITabNavigation[] TabNavigations { get; }
        public int SelectedIndex { get; }
        public string TabbedPageName { get; }

        public TabbedNavigation(int selectedIndex = -1, params ITabNavigation[] tabNavigations) : this(nameof(TabbedPage), selectedIndex, tabNavigations)
        {
        }

        public TabbedNavigation(string tabbedPageName, int selectedIndex = -1, params ITabNavigation[] tabNavigations)
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

        public string CreateNavigationUri(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
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

            var uri = TabbedPageName;

            if (queries.Count > 0)
            {
                uri += "?" + string.Join("&", queries.Select(pair => $"{pair.Key}={pair.Value}"));
            }

            if (NextNavigation != null)
            {
                uri += "/" + NextNavigation.CreateNavigationUri(parameters, nextQueries);
            }

            return uri;
        }
    }

    public class TabbedNavigation<TViewModel> : TabbedNavigation where TViewModel : INavigationViewModel
    {
        public TabbedNavigation(int selectedIndex = -1, params ITabNavigation[] tabNavigations) : base(NavigationNameProvider.GetNavigationName(typeof(TViewModel)), selectedIndex, tabNavigations)
        {
        }
    }
}
