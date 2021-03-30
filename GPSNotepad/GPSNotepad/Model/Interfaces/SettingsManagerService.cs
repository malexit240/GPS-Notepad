using GPSNotepad.Resources;
using GPSNotepad.Styles;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GPSNotepad.Model.Interfaces
{
    public class SettingsManagerService : ISettingsManagerService
    {
        public Theme Theme
        {
            get => (Theme)Preferences.Get(nameof(Theme), (int)Theme.Light);
            set
            {
                Preferences.Set(nameof(Theme), (int)value);
                App.Current.Resources.MergedDictionaries.Clear();
                if (value == Theme.Light)
                    App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                else
                    App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
            }
        }

        public CultureInfo Language
        {
            get => new CultureInfo(Preferences.Get(nameof(Language), "en_US"));
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

        public void Init()
        {
            Theme = Theme;
            Language = Language;
        }
    }
}
