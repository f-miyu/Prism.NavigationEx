using System;
using Prism.Navigation;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Prism.Common;

namespace Prism.NavigationEx
{
    public static class NavigationParametersExtensions
    {
        public static bool CreateTabExists(this INavigationParameters self, string name)
        {
            var createTabs = self.GetValues<string>(KnownNavigationParameters.CreateTab);
            return createTabs.Select(s =>
            {
                var segments = s.Split('|');
                if (segments.Length > 1)
                {
                    s = segments[1];
                }
                return UriParsingHelper.GetSegmentName(s);
            }).Any(s => s == name);
        }

        public static IEnumerable<string> GetCreateTabParameters(this INavigationParameters self, string name, string parameterKey)
        {
            var createTabs = self.GetValues<string>(KnownNavigationParameters.CreateTab);
            return createTabs
                .Select(s =>
                {
                    var segments = s.Split('|');
                    if (segments.Length > 1)
                    {
                        s = segments[1];
                    }
                    return (Name: UriParsingHelper.GetSegmentName(s), Parameters: UriParsingHelper.GetSegmentParameters(s));
                })
                .Where(t => t.Name == name)
                .Select(t => (Found: t.Parameters.TryGetValue<string>(parameterKey, out var value), Value: value))
                .Where(t => t.Found)
                .Select(t => t.Value);
        }
    }
}
