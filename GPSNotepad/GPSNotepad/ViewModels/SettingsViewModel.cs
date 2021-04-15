using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Settings;
using GPSNotepad.Styles;

namespace GPSNotepad.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region ---Public Properties---
        ISettingsManagerService _settingsManager;


        private string _themeName = "";
        public string ThemeName
        {
            get => _themeName;
            set => SetProperty(ref _themeName, value);
        }

        public bool IsLightTheme
        {
            get => _settingsManager.Theme == Theme.Light;
            set
            {
                _settingsManager.Theme = value ? Theme.Light : Theme.Dark;
                ThemeName = value ? TextResources["LightTheme"] : TextResources["DarkTheme"];
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

        public ICommand CheckedEnglish =>
        new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("en-US"));
        public ICommand CheckedRussian =>
        new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("ru-RU"));
        #endregion

        #region ---Connstructors---
        public SettingsViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
           : base(navigationService)
        {
            this._settingsManager = settingsManager;

        }
        #endregion
    }
}
