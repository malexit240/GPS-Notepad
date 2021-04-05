using System.Collections.Generic;
using GPSNotepad.Model.Entities;

namespace GPSNotepad.Model
{
    public struct PinsStateChangedMessage
    {
        public PinsStateChangedMessage(IList<Pin> newPins, PinsStateChangedType changedType)
        {
            NewPins = newPins;
            ChangedType = changedType;
        }

        public IList<Pin> NewPins { get; set; }

        public PinsStateChangedType ChangedType { get; set; }

        public Pin ChangedPin { get => NewPins[0]; }
    }
}
