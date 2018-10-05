using System;
using Prism.Navigation;
using Reactive.Bindings;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class FirstTabPageViewModel : NavigationViewModel<string, string>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand GoToSecondPageCommand { get; } = new AsyncReactiveCommand();

        public FirstTabPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSecondPageCommand.Subscribe(async () =>
            {
                var result = await NavigateAsync<SecondPageViewModel, string>();
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });
        }

        public override void Prepare(string parameter)
        {
            Text.Value = parameter;
        }
    }
}
