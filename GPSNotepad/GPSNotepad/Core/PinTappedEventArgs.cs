using Xamarin.Forms.GoogleMaps;
using System;

namespace GPSNotepad
{
    public class PinTappedEventArgs : EventArgs
    {
        public Pin Pin { get; set; }

        public PinTappedEventArgs(Pin pin) => Pin = pin;
    }
}
