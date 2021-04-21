using System.Globalization;
using GPSNotepad.Styles;

namespace GPSNotepad.Services.Settings
{
    public interface ISettingsManagerService
    {
        Theme Theme { get; set; }
        CultureInfo Language { get; set; }
        bool IsAuthorized { get; set; }
        string SessionToken { get; set; }
        void Init();
    }
}
