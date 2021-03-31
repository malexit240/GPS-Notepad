using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using GPSNotepad.Model;
using GPSNotepad.Database;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Container.Resolve<ISettingsManagerService>().Init();
            //Authorizator.ContinueSessionAsync();

            if (CurrentUser.Instance != null)
                await NavigationService.NavigateAsync("NavigationPage/MainPage");
            else
                await NavigationService.NavigateAsync("NavigationPage/SignInPage");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterInstance<IAuthorizatorService>(new DBAuthorizatorService());
            containerRegistry.RegisterInstance<IRegistratorService>(new DBRegistratorService());
            containerRegistry.RegisterInstance<IPinService>(new PinStateService());

            containerRegistry.RegisterInstance<IPermanentPinService>(new DBPinService());

            containerRegistry.RegisterInstance<ISettingsManagerService>(new SettingsManagerService());
            containerRegistry.RegisterInstance<ISecureStorageService>(new SecureStorageService());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMapPage, MainMapViewModel>();
            containerRegistry.RegisterForNavigation<ListOfPinspage, ListOfPinsViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();


        }
    }
}
