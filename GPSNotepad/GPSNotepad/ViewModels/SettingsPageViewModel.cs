using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using GPSNotepad.Model.Interfaces;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
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

        public SettingsPageViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
            : base(navigationService)
        {
            this._settingsManager = settingsManager;

            CheckedEnglish = new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("en-US"));
            CheckedRussian = new DelegateCommand(() => _settingsManager.Language = new System.Globalization.CultureInfo("ru-RU"));
        }
    }
}
