using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GPSNotepad.Resources
{
    public class CultureChangedMessage
    {
        #region ---Constructors---
        public CultureChangedMessage(CultureInfo cultureInfo) => NewCultureInfo = cultureInfo;
        #endregion
        #region ---Public Properties---
        public CultureInfo NewCultureInfo { get; set; }
        #endregion

    }
}
