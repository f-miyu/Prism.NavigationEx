using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    internal static class TabbedNavigationPathHelper
    {
        public static void SetParameters(ref NavigationParameters parameters, ref NavigationParameters queries, ITab[] tabs)
        {
            if (queries == null)
            {
                queries = new NavigationParameters();
            }

            if (tabs != null)
            {
                foreach (var tab in tabs)
                {
                    queries.Add(KnownNavigationParameters.CreateTab, tab.GetPath(ref parameters));
                }
            }
        }
    }
}
