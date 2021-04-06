using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Extensions;
using Xamarin.Forms;

namespace GPSNotepad
{
    class PinsManyMaps
    {
        #region ---Constructors---
        public PinsManyMaps(UniqueObservableCollection<PinViewModel> pins, BindableMap map)
        {
            Pins = pins;
            Maps = new List<BindableMap>();
            Maps.Add(map);
        }

        public PinsManyMaps(UniqueObservableCollection<PinViewModel> pins)
        {
            Pins = pins;
            Maps = new List<BindableMap>();
        }
        #endregion

        #region ---Properties---
        public UniqueObservableCollection<PinViewModel> Pins { get; private set; }

        public IList<BindableMap> Maps { get; set; }
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
        private void UpdatePinsSource(BindableMap bindableMap, UniqueObservableCollection<PinViewModel> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
            {

                var p = pin.GetGoogleMapsPin();
                p.Type = PinType.Place;
                if (pin.Favorite == true)
                    p.Icon = BitmapDescriptorFactory.FromBundle("purpleMarkerSmallWithCrown.png");
                else
                    p.Icon = BitmapDescriptorFactory.FromBundle("green_grayMarkerSmall.png");
                if (bindableMap.ShowInfoWindow == false)
                    p.Label = pin.PinId.ToString();
                bindableMap.Pins.Add(p);
            }

        }
        #endregion
    }
}
