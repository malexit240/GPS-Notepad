using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Settings;

namespace GPSNotepad.ViewModels
{
    public class LanguageSettingsViewModel : ViewModelBase
    {
        #region ---Connstructors---
        public LanguageSettingsViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
           : base(navigationService) => this.SettingsManager = settingsManager;
        #endregion

        #region ---Public Properties---
        protected ISettingsManagerService SettingsManager { get; set; }

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