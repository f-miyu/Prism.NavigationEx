using System;
using Prism.Navigation;
using Reactive.Bindings;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class ThirdPageViewModel : NavigationViewModel<int, int>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();
        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();

        public ThirdPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OkCommand.Subscribe(() => GoBackToRootAsync(100));
            CancelCommand.Subscribe(() => GoBackToRootAsync());
        }

        public override void Prepare(int parameer)
        {
            Text.Value = parameer.ToString();
        }
    }
}
