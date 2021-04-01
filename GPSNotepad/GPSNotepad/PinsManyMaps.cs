using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad
{
    class PinsManyMaps
    {
        #region ---Constructors---
        public PinsManyMaps(ICollection<Pin> pins, Map map)
        {
            Pins = pins;
            Maps = new List<Map>();
            Maps.Add(map);
        }

        public PinsManyMaps(ICollection<Pin> pins)
        {
            Pins = pins;
            Maps = new List<Map>();
        }
        #endregion

        #region ---Properties---
        public ICollection<Pin> Pins { get; private set; }

        public IList<Map> Maps { get; set; }
        #endregion

        #region ---Public Methods---
        public static PinsManyMaps GetEquivalent(ICollection<Pin> pins) => new PinsManyMaps(pins);

        public void UpdateMaps()
        {
            foreach (var map in Maps)
                UpdatePinsSource(map, Pins);
        }
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Pins);

        #endregion

        #region ---Private Helpers---
        private void UpdatePinsSource(Map bindableMap, ICollection<Pin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
                bindableMap.Pins.Add(pin);
        }
        #endregion
    }
}
