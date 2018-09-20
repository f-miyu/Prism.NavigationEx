using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class MainPageViewModel : NavigationViewModel, IConfirmNavigationAsync
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand ShowSecondPpageCommand { get; } = new AsyncReactiveCommand();

        private CancellationTokenSource _cts;

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ShowSecondPpageCommand.Subscribe(async () =>
            {
                //_cts = new CancellationTokenSource();
                //var result = await NavigationService.NavigateAsync<SecondPageViewModel, string, string>(Text.Value, false, wrapInNavigationPage: false, noHistory: false, cancellationToken: _cts.Token);
                //if (result.Success)
                //{
                //    Text.Value = result.Data;
                //}

                var result = await NavigationService.NavigateAsync<SecondPageViewModel, string, string>(Text.Value, false, true, false, false, new Navigation<ThirdPageViewModel, int, int> { Parameter = 5 }, new Navigation<SecondPageViewModel, string, string> { Parameter = "vvv" });
            });
        }

        public async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            // _cts?.Cancel();
            return true;
        }
    }
}
