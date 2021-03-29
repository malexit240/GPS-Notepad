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

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterInstance<IAuthorizatorService>(new BufferedAuthorizatorService());
            containerRegistry.RegisterInstance<IRegistratorService>(new BufferedRegistratorService());
            containerRegistry.RegisterInstance<IPinService>(new BufferedPinService());

            containerRegistry.RegisterInstance<IPermanentPinService>(new DBPinService());
        }
    }
}
