using System;
using Prism.Navigation;
using Reactive.Bindings;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class SecondPageViewModel : NavigationViewModelResult<string>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();
        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();
        public AsyncReactiveCommand ShowThirdPageCommand { get; } = new AsyncReactiveCommand();

        public SecondPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OkCommand.Subscribe(() => GoBackAsync());
            CancelCommand.Subscribe(() => GoBackAsync());

            ShowThirdPageCommand.Subscribe(async () =>
            {
                var result = await NavigateAsync<ThirdPageViewModel, int, int>(10);
                if (result.Success)
                {
                    Text.Value = result.Data.ToString();
                }
            });
        }
    }
}
