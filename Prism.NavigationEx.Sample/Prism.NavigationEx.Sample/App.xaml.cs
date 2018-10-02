using Prism;
using Prism.Ioc;
using Prism.NavigationEx.Sample.ViewModels;
using Prism.NavigationEx.Sample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Prism.NavigationEx.Sample
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync<MainPageViewModel>(wrapInNavigationPage: true);

            await NavigationService.NavigateAsync(NavigationUriFactory.CreateForTabbedPage(1, new TabNavigation<MainPageViewModel>(true), new TabNavigation<SecondPageViewModel>(true), new TabNavigation<SecondPageViewModel>(true)));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(this);
            containerRegistry.RegisterForNavigation<TabbedPage>();
        }
    }
}
