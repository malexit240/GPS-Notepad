using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad
{
    class PinsManyMaps
    {
        #region ---Constructors---
        public PinsManyMaps(UniqueObservableCollection<Pin> pins, Map map)
        {
            Pins = pins;
            Maps = new List<Map>();
            Maps.Add(map);
        }

        public PinsManyMaps(UniqueObservableCollection<Pin> pins)
        {
            Pins = pins;
            Maps = new List<Map>();
        }
        #endregion

        #region ---Properties---
        public UniqueObservableCollection<Pin> Pins { get; private set; }

        public IList<Map> Maps { get; set; }
        #endregion

        #region ---Public Methods---
        public static PinsManyMaps GetEquivalent(UniqueObservableCollection<Pin> pins) => new PinsManyMaps(pins);

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
        private void UpdatePinsSource(Map bindableMap, UniqueObservableCollection<Pin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
                bindableMap.Pins.Add(pin);
        }
        #endregion
    }
}
