using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Threading.Tasks;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class MainPageViewModel : NavigationViewModel
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand GoToSecondPpageCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoToThirdPageCommand { get; } = new AsyncReactiveCommand();

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSecondPpageCommand.Subscribe(async () =>
            {
                var result = await NavigationService.NavigateAsync<SecondPageViewModel, string>();
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });

            GoToThirdPageCommand.Subscribe(async () =>
            {
                var navigationPath = NavigationPath.Create<SecondPageViewModel, string>((viewModel, thirdPageResult) =>
                {
                    if (thirdPageResult.Success)
                    {
                        viewModel.Text.Value = thirdPageResult.Data;
                    }
                }).Add<ThirdPageViewModel, string>(Text.Value);

                var result = await NavigationService.NavigateAsync<SecondPageViewModel, string>(navigationPath);
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });
        }
    }
}
