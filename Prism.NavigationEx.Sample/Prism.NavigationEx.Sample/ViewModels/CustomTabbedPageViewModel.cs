using System;
using Prism.Navigation;
using Xamarin.Forms;
using Reactive.Bindings;
using System.Threading.Tasks;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class CustomTabbedPageViewModel : NavigationViewModel<int, int>
    {
        public ReactivePropertySlim<int> SelectedIndex { get; } = new ReactivePropertySlim<int>();

        public CustomTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        public override void Prepare(int parameter)
        {
            SelectedIndex.Value = parameter;
        }

        public override Task<bool> CanNavigateAsync(NavigationParameters parameters)
        {
            return base.CanNavigateAsync(parameters);
        }
    }
}

