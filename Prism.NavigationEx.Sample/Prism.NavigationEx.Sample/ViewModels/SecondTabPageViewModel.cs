using System;
using Prism.Navigation;
using Reactive.Bindings;
using System.Threading.Tasks;
using Prism.Services;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class SecondTabPageViewModel : NavigationViewModel
    {
        public AsyncReactiveCommand GoToMainPageCommand { get; } = new AsyncReactiveCommand();

        public SecondTabPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToMainPageCommand.Subscribe(async () =>
            {
                await NavigateAsync<MainPageViewModel>(wrapInNavigationPage: true, noHistory: true);
            });
        }
    }
}
