using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using GPSNotepad.Model;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.Settings;
using GPSNotepad.Services.SecureStorageService;
using GPSNotepad.Services.PinService;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });// Remake as View with button and label
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            Container.Resolve<ISettingsManagerService>().Init();

            var authorizationService = Container.Resolve<IAuthorizationService>();
            var authenticationService = Container.Resolve<IAuthenticationService>();
            var secureStorage = Container.Resolve<ISecureStorageService>();

            if (authorizationService.IsAuthorized && authenticationService.ContinueSession(secureStorage.SessionToken))
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInPage)}");

            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISecureStorageService>
                (Container.Resolve<SecureStorageService>());
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());

            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinPage, AddEditPinPageViewModel>();
        }
    }
}
