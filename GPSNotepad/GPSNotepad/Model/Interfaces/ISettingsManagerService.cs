using System.Globalization;

namespace GPSNotepad.Model.Interfaces
{
    public interface ISettingsManagerService
    {
        Theme Theme { get; set; }
        CultureInfo Language { get; set; }
        bool IsAuthorized { get; set; }
        void Init();
    }
}
