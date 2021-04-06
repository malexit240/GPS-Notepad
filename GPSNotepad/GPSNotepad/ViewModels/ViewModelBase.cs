using Prism.Mvvm;
using Prism.Navigation;
using GPSNotepad.Resources;
using GPSNotepad.Services.Settings;

namespace GPSNotepad.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        public LocalizedResources TextResources { get; private set; }

        #region ---Constructors---
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            TextResources = new LocalizedResources(typeof(GPSNotepad.Resources.UIResources), (App.Current.Container.Resolve<ISettingsManagerService>()).Language);
        }
        #endregion

        #region ---Overrides---
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
        #endregion
    }
}
