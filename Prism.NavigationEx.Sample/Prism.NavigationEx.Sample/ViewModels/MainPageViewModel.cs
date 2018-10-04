using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prism.Services;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class MainPageViewModel : NavigationViewModel
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand GoToSecondPageCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoToThirdPageCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoToTabbedPageCommand { get; } = new AsyncReactiveCommand();

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            GoToSecondPageCommand.Subscribe(async () =>
            {
                var result = await NavigationService.NavigateAsync<SecondPageViewModel, string>();
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });

            GoToThirdPageCommand.Subscribe(async () =>
            {
                var navigation = NavigationFactory.Create<SecondPageViewModel, string>((viewModel, thirdPageResult) =>
                {
                    if (thirdPageResult.Success && viewModel is SecondPageViewModel secondPageViewModel)
                    {
                        secondPageViewModel.Text.Value = thirdPageResult.Data;
                    }
                }).Add<ThirdPageViewModel>(() => pageDialogService.DisplayAlertAsync("Are you sure?", null, "Yes", "No"));

                var result = await NavigationService.NavigateAsync<SecondPageViewModel, string>(navigation);
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });

            GoToTabbedPageCommand.Subscribe(async () =>
            {
                var navigation = NavigationFactory.Create(nameof(TabbedPage), new Tab<FirstTabPageViewModel, string>(Text.Value, true), new Tab<SecondTabPageViewModel>(true));

                await NavigationService.NavigateAsync(navigation, noHistory: true);
            });
        }
    }
}
