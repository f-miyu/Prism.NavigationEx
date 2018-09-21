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
    public class MainPageViewModel : NavigationViewModel
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand GoToSecondPpageCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand DeepLinkCommand { get; } = new AsyncReactiveCommand();

        private CancellationTokenSource _cts;

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSecondPpageCommand.Subscribe(async () =>
            {
                var result = await NavigateAsync<SecondPageViewModel, string>(false, true, false, false);
                if (result.Success)
                {
                    Text.Value = result.Data;
                }

                // var result = await NavigationService.NavigateAsync<SecondPageViewModel, string>(false, true, false, false, null, new Navigation<ThirdPageViewModel, int> { Parameter = 5 }, new Navigation<SecondPageViewModel, string> { Parameter = "vvv" });
            });

            DeepLinkCommand.Subscribe(async () =>
            {

            });
        }
    }
}
