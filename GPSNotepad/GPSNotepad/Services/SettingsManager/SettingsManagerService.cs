﻿using GPSNotepad.PlatformDependencyInterfaces;
using GPSNotepad.Resources;
using GPSNotepad.Styles;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GPSNotepad.Services.Settings
{
    public class SettingsManagerService : ISettingsManagerService
    {
        #region ---ISettingsManagerService Implementation---
        public Theme Theme
        {
            get => (Theme)Preferences.Get(nameof(Theme), (int)Theme.Light);
            set
            {
                Preferences.Set(nameof(Theme), (int)value);
                ChangeTheme(value);
            }
        }

        public CultureInfo Language
        {
            get => new CultureInfo(Preferences.Get(nameof(Language), "en-US"));
            set
            {
                Preferences.Set(nameof(Language), value.Name);
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(value));
            }
        }

        public bool IsAuthorized
        {
            get => Preferences.Get(nameof(IsAuthorized), false);
            set => Preferences.Set(nameof(IsAuthorized), value);
        }
        #endregion

        #region ---Public Methods---
        public void Init()
        {
            Theme = Theme;
            Language = Language;
        }
        #endregion

        #region ---Private Helpers---
        private void ChangeTheme(Theme theme)
        {
            App.Current.Resources.MergedDictionaries.Clear();
            if (theme == Theme.Light)
                App.Current.Resources.MergedDictionaries.Add(new LightTheme());
            else
                App.Current.Resources.MergedDictionaries.Add(new DarkTheme());

            DependencyService.Get<IChangerBarColor>().SetBarColor((Color)App.Current.Resources["PrimaryColor"]);
        }
        #endregion

    }
}
