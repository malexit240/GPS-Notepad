using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using GPSNotepad.Model;
using GPSNotepad.Database;
using GPSNotepad.Model.Interfaces;
using GPSNotepad.Repositories;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });// Remake as View with button and label
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Container.Resolve<ISettingsManagerService>().Init();
            Authorizator.ContinueSession();// Remake as One Service

            if (CurrentUser.Instance != null)//Remake througnt IAuthorizationService
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
            else
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInPage)}");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAuthorizatorService>(Container.Resolve<DBAuthorizatorService>());
            containerRegistry.RegisterInstance<IRegistratorService>(Container.Resolve<DBRegistratorService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinStateService>());

            containerRegistry.RegisterInstance<IPermanentPinService>(Container.Resolve<DBPinService>());

            containerRegistry.RegisterInstance<ISettingsManagerService>(Container.Resolve<SettingsManagerService>());
            containerRegistry.RegisterInstance<ISecureStorageService>(Container.Resolve<SecureStorageService>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPinPage, AddEditPinPageViewModel>();


        }
    }
}
