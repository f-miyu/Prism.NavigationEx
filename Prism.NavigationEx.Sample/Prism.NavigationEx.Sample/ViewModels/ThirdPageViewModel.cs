using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Reactive.Bindings;
using Prism.Services;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class ThirdPageViewModel : ConfirmNavigationViewModel<string, string, bool>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand OkCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand CancelCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoBackToMainPageCommand { get; } = new AsyncReactiveCommand();

        private readonly IPageDialogService _pageDialogService;

        public ThirdPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;

            OkCommand.Subscribe(async () => await this.GoBackWithConfirmAsync(Text.Value, false));
            CancelCommand.Subscribe(async () => await this.GoBackWithConfirmAsync(false));
            GoBackToMainPageCommand.Subscribe(async () => await this.GoBackToRootWithConfirmAsync(Text.Value, true));
        }

        public override void Prepare(string parameter)
        {
            Text.Value = parameter;
        }

        public override Task<bool> CanNavigateAtBackAsync(bool parameter)
        {
            if (parameter)
            {
                return _pageDialogService.DisplayAlertAsync("Are you sure?", null, "Yes", "No");
            }
            return Task.FromResult(true);
        }
    }
}
