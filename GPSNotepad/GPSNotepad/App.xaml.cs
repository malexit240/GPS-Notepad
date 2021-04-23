using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.Settings;
using GPSNotepad.Services.SecureStorageService;
using GPSNotepad.Services.PinService;
using GPSNotepad.Services.QRCodeService;
using GPSNotepad.Services.PlaceEventsService;
using GPSNotepad.Services.PermissionManager;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer) : base(initializer)
        { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var NotificationJobManager = new NotificationJobManager();

            Container.Resolve<ISettingsManagerService>().Init();

            var authorizationService = Container.Resolve<IAuthorizationService>();
            var authenticationService = Container.Resolve<IAuthenticationService>();
            var secureStorage = Container.Resolve<ISecureStorageService>();

            if (authorizationService.IsAuthorized && authenticationService.ContinueSession(secureStorage.SessionToken))
            {
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");

            }
            else
            {
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartPage)}");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISecureStorageService>(Container.Resolve<SecureStorageService>());
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());

            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IPlaceEventsService>(Container.Resolve<PlaceEventsService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());
            containerRegistry.RegisterInstance<IPermissionManager>(Container.Resolve<PermissionManager>());
            containerRegistry.RegisterInstance<IQrScanerService>(Container.Resolve<QrScanerService>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpOnePage, SignUpOneViewModel>();
            containerRegistry.RegisterForNavigation<SignUpTwoPage, SignUpTwoViewModel>();

            containerRegistry.RegisterForNavigation<MainTabbedPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<AddEditPinAndEventsTabbedPage, AddEditPinAndEventsViewModel>();
            containerRegistry.RegisterForNavigation<QRCodeModalPage, QRCodeModalViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPlaceEventPage, AddEditPlaceEventViewModel>();
        }
    }
}
