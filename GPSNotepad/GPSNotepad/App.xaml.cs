using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using GPSNotepad.Model;
using GPSNotepad.Database;
using GPSNotepad.Model.Interfaces;

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

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterInstance<IAuthorizatorService>(new DBAuthorizatorService());
            containerRegistry.RegisterInstance<IRegistratorService>(new DBRegistratorService());
            containerRegistry.RegisterInstance<IPinService>(new PinStateService());

            containerRegistry.RegisterInstance<IPermanentPinService>(new DBPinService());

            containerRegistry.RegisterInstance<ISettingsManagerService>(new SettingsManagerService());

            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMapPage, MainMapViewModel>();
            containerRegistry.RegisterForNavigation<ListOfPinspage, ListOfPinsViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}
