using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.Settings;
using GPSNotepad.Services.PinService;
using GPSNotepad.Services.QRCodeService;
using GPSNotepad.Services.PlaceEventsService;
using GPSNotepad.Services.PermissionManager;
using GPSNotepad.Services.NotificationService;
using Xamarin.Essentials;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer) : base(initializer)
        { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var NotificationJobManager = new NotificationJobManager();

            var settingsManger = Container.Resolve<ISettingsManagerService>();

            settingsManger.Init();

            var authorizationService = Container.Resolve<IAuthorizationService>();
            var authenticationService = Container.Resolve<IAuthenticationService>();


            if (authorizationService.IsAuthorized && authenticationService.ContinueSession(settingsManger.SessionToken))
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");

            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartPage)}");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
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
            containerRegistry.RegisterForNavigation<ScanQRModalPage, ScanQRViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPlaceEventPage, AddEditPlaceEventViewModel>();

            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<LanguageSettingsPage, LanguageSettingsViewModel>();
        }
    }
}
