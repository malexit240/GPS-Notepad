using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Settings;
using GPSNotepad.Styles;
using GPSNotepad.Views;

namespace GPSNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region ---Connstructors---

        public SettingsPageViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
           : base(navigationService) => this.SettingsManager = settingsManager;

        #endregion

        #region ---Public Properties---

        protected ISettingsManagerService SettingsManager { get; set; }

        public bool IsDarkTheme
        {
            get => SettingsManager.Theme == Theme.Dark;
            set
            {
                SettingsManager.Theme = value ? Theme.Dark : Theme.Light;
                RaisePropertyChanged(nameof(IsDarkTheme));
            }
        }

        #endregion

        #region ---Commands---

        private ICommand _onLanguageTabTapped;
        public ICommand OnLanguageTabTapped => _onLanguageTabTapped ??= new DelegateCommand(() => NavigationService.NavigateAsync(nameof(LanguageSettingsPage)));

        #endregion
    }
}