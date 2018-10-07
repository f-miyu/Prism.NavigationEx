using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationViewModelExtensions
    {
        internal static void PrepareIfNeeded<TParameter>(this INavigationViewModel<TParameter> self, INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                var navigationName = NavigationNameProvider.GetNavigationName(self.GetType());
                if (parameters.CreateTabExists(navigationName))
                {
                    var ids = parameters.GetCreateTabParameters(navigationName, NavigationParameterKey.ParameterId);
                    foreach (var id in ids)
                    {
                        var key = "_" + id;
                        if (!parameters.ContainsKey(key) && parameters.TryGetValue<TParameter>(id, out var parameter))
                        {
                            self.Prepare(parameter);
                            parameters.Add(key, null);
                            break;
                        }
                    }
                }
                else
                {
                    if (parameters.TryGetValue<string>(NavigationParameterKey.ParameterId, out var id))
                    {
                        if (parameters.TryGetValue<TParameter>(id, out var parameter))
                        {
                            self.Prepare(parameter);
                        }
                    }
                }
            }
        }
    }
}
