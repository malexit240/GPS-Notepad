using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        ISettingsManagerService _settingsManager;

        public bool IsLightTheme
        {
            get => _settingsManager.Theme == Theme.Light;
           // set => SetProperty(,) _settingsManager.Theme = value;
        }

        public SettingsPageViewModel(INavigationService navigationService, ISettingsManagerService settingsManager)
            : base(navigationService)
        {
            this._settingsManager = settingsManager;
        }
    }
}
