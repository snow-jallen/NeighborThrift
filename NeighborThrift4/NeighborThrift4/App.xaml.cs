using Prism;
using Prism.Ioc;
using NeighborThrift4.ViewModels;
using NeighborThrift4.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NeighborThrift4.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NeighborThrift4
{
    public partial class App
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
            //InitializeComponent();

            NavigationService.NavigateAsync(nameof(MenuPage) + "/" + nameof(NavigationPage) + "/" + nameof(MainPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailPage, DetailPageViewModel>();
			containerRegistry.RegisterForNavigation<SecondPage, SecondPageViewModel>();
			containerRegistry.RegisterForNavigation<ThirdPage, ThirdPageViewModel>();
			containerRegistry.Register<IDataService, SqliteDataService>();
			containerRegistry.Register<INotificationService, NotificationService>();
		}
    }
}
