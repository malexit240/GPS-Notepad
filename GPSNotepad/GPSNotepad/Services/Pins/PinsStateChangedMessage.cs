using System.Collections.Generic;
using GPSNotepad.Entities;
using GPSNotepad.Enums;

namespace GPSNotepad.Services.PinService
{
    public struct PinsStateChangedMessage
    {
        #region ---Constructors---

        public PinsStateChangedMessage(IList<Pin> newPins, PinsStateChangedType changedType)
        {
            NewPins = newPins;
            ChangedType = changedType;
        }

        #endregion

        #region ---Public Fields---

        public IList<Pin> NewPins;

        public PinsStateChangedType ChangedType;

        #endregion

        #region ---Public Properties---

        public Pin ChangedPin { get => NewPins[0]; }

        #endregion
    }
}
