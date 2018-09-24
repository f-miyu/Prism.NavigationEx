using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationWithConfirmViewModelExtensions
    {
        internal static async Task<bool> CanNavigateInternalAsync(this INavigationWithConfirmViewModel self, INavigationParameters parameters)
        {
            var result = true;

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                result = await self.CanNavigateAtBackAsync().ConfigureAwait(false);
            }
            else
            {
                result = await self.CanNavigateAtNewAsync().ConfigureAwait(false);

                if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
                {
                    cts.Cancel();
                }
            }

            return result;
        }
    }
}
