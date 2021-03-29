using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using GPSNotepad.Core;
using GPSNotepad.DatabaseMocks.UserMocks;
using GPSNotepad.DatabaseMocks.PinMocks;


namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterInstance<IAuthorizatorService>(new DBAuthorizatorService());
            containerRegistry.RegisterInstance<IRegistratorService>(new DBRegistratorService());
            containerRegistry.RegisterInstance<IPinService>(new DBPinService());
        }
    }
}
