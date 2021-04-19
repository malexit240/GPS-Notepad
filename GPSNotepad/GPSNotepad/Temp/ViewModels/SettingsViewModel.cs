using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Settings;
using GPSNotepad.Styles;

namespace GPSNotepad.Temp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region ---Connstructors---
        public SettingsViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
           : base(navigationService) => this.SettingsManager = settingsManager;
        #endregion

        #region ---Public Properties---
        protected ISettingsManagerService SettingsManager { get; set; }

        private string _themeName = "";
        public string ThemeName
        {
            get => _themeName;
            set => SetProperty(ref _themeName, value);
        }

        public bool IsLightTheme
        {
            get => SettingsManager.Theme == Theme.Light;
            set
            {
                SettingsManager.Theme = value ? Theme.Light : Theme.Dark;
                ThemeName = value ? TextResources["LightTheme"] : TextResources["DarkTheme"];
                RaisePropertyChanged(nameof(IsLightTheme));
            }
        }

        public bool IsEnglish => SettingsManager.Language.Name == "en-US";

        public bool IsRussian => SettingsManager.Language.Name == "ru-RU";
        #endregion

        #region ---Commands---
        private ICommand _checkedEnglish;
        public ICommand CheckedEnglish => _checkedEnglish ??=
        new DelegateCommand(() => SettingsManager.Language = new System.Globalization.CultureInfo("en-US"));

        private ICommand _checkedRussian;
        public ICommand CheckedRussian => _checkedRussian ??=
        new DelegateCommand(() => SettingsManager.Language = new System.Globalization.CultureInfo("ru-RU"));
        #endregion
    }
}
