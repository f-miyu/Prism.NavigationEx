using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    internal static class NavigationViewModelExtensions
    {
        public static void PrepareIfNeeded<TParameter>(this INavigationViewModel<TParameter> self, INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.TryGetValue<string>(NavigationParameterKey.ParameterId, out var id))
                {
                    if (parameters.TryGetValue<TParameter>(id, out var parameter))
                    {
                        self.Prepare(parameter);
                    }
                }
                else if (parameters.TryGetValue<TParameter>(NavigationParameterKey.Parameter, out var parameter))
                {
                    self.Prepare(parameter);
                }
            }
        }
    }
}
