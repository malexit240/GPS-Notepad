using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GPSNotepad.Resources
{
    public class CultureChangedMessage
    {
        public CultureInfo NewCultureInfo { get; set; }
        public CultureChangedMessage(CultureInfo cultureInfo) => NewCultureInfo = cultureInfo;

    }
}
