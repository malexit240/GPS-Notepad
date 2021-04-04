using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Extensions;

namespace GPSNotepad
{
    class PinsManyMaps
    {
        #region ---Constructors---
        public PinsManyMaps(UniqueObservableCollection<PinViewModel> pins, Map map)
        {
            Pins = pins;
            Maps = new List<Map>();
            Maps.Add(map);
        }

        public PinsManyMaps(UniqueObservableCollection<PinViewModel> pins)
        {
            Pins = pins;
            Maps = new List<Map>();
        }
        #endregion

        #region ---Properties---
        public UniqueObservableCollection<PinViewModel> Pins { get; private set; }

        public IList<Map> Maps { get; set; }
        #endregion

        #region ---Public Methods---
        public static PinsManyMaps GetEquivalent(UniqueObservableCollection<PinViewModel> pins) => new PinsManyMaps(pins);

        public void UpdateMaps()
        {
            foreach (var map in Maps)
                UpdatePinsSource(map, Pins);
        }
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => Pins.GetHashCode();

        #endregion

        #region ---Private Helpers---
        private void UpdatePinsSource(Map bindableMap, UniqueObservableCollection<PinViewModel> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
                bindableMap.Pins.Add(pin.GetGoogleMapsPin());
        }
        #endregion
    }
}
