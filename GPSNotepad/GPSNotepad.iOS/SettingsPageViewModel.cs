using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Settings;
using GPSNotepad.Styles;

namespace GPSNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region ---Public Properties---
        ISettingsManagerService _settingsManager;

        public bool IsLightTheme
        {
            get => _settingsManager.Theme == Theme.Light;
            set
            {
                _settingsManager.Theme = value ? Theme.Light : Theme.Dark;
                RaisePropertyChanged(nameof(IsLightTheme));
            }
        }

        public bool IsEnglish
        {
            get => _settingsManager.Language.Name == "en-US";
        }

        public bool IsRussian
        {
            get => _settingsManager.Language.Name == "ru-RU";
        }

        public ICommand CheckedEnglish { get; set; }
        public ICommand CheckedRussian { get; set; }
        #endregion

        #region ---Connstructors---
        public SettingsPageViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
           : base(navigationService)
        {
            this._settingsManager = settingsManager;

            CheckedEnglish = new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("en-US"));
            CheckedRussian = new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("ru-RU"));
        }
        #endregion
    }
}
