using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
