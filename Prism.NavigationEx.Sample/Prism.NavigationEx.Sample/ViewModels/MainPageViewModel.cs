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

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSecondPpageCommand.Subscribe(async () =>
            {
                var result = await this.NavigateAsync<SecondPageViewModel, string>(false, true, false, false);
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });

            DeepLinkCommand.Subscribe(async () =>
            {
                var result = await this.NavigateAsync<SecondPageViewModel, string>(false, true, false, false,
                                                                                   new Navigation<ThirdPageViewModel, string> { Parameter = Text.Value });
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });
        }

        public override void OnNavigatingFrom(INavigationParameters parameters)
        {
            base.OnNavigatingFrom(parameters);
        }
    }
}
