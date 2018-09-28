using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Reactive.Bindings;
using Prism.Services;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class ThirdPageViewModel : NavigationViewModel<string, string>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand OkCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand CancelCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoBackToMainPageCommand { get; } = new AsyncReactiveCommand();

        private readonly IPageDialogService _pageDialogService;

        public ThirdPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;

            OkCommand.Subscribe(async () => await NavigationService.GoBackAsync(this, Text.Value, canNavigate: () => pageDialogService.DisplayAlertAsync("Are you sure?", Text.Value, "Yes", "No")));
            CancelCommand.Subscribe(async () => await NavigationService.GoBackAsync());
            GoBackToMainPageCommand.Subscribe(async () => await NavigationService.GoBackToRootAsync());
        }

        public override void Prepare(string parameter)
        {
            Text.Value = parameter;
        }
    }
}
