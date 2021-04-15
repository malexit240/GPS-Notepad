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

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        NotificationJobManager NotificationJobManager;

        protected override void OnInitialized()
        {
            InitializeComponent();

            NotificationJobManager = new NotificationJobManager();

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
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISecureStorageService>
                (Container.Resolve<SecureStorageService>());
            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());

            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IPlaceEventsService>(Container.Resolve<PlaceEventsService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());
            containerRegistry.RegisterInstance<IQrScanerService>(Container.Resolve<QrScanerService>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinPage, AddEditPinPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinAndEventsCarouselPage, AddEditPinAndEventsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPlaceEventPage, AddEditPlaceEventPageViewModel>();
            containerRegistry.RegisterForNavigation<QRCodeModalPage, QRCodeModalViewModel>();
        }
    }
}
