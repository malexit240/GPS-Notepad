using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad
{
    public class BindableMap : Map
    {
        #region ---Source Properties---
        public UniqueObservableCollection<Pin> PinsSource
        {
            get => (UniqueObservableCollection<Pin>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public MapSpan MapSpan
        {
            get => (MapSpan)GetValue(MapSpanProperty);
            set => SetValue(MapSpanProperty, value);
        }
        #endregion

        #region ---Bindable Source Properties---
        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
                                                         propertyName: "PinsSource",
                                                         returnType: typeof(UniqueObservableCollection<Pin>),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: "MapSpan",
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: MapSpanPropertyChanged);

        private static List<PinsManyMaps> PinsMapsScope { get; set; } = new List<PinsManyMaps>();
        #endregion

        #region ---Event Handlers---
        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newPinsSource = newValue as UniqueObservableCollection<Pin>;

            if (thisInstance == null ||
                newPinsSource == null)
                return;

            AddMapToPinsCollection(thisInstance, newPinsSource);
        }

        private static void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var pins = sender as UniqueObservableCollection<Pin>;

            PinsManyMaps _pinMap = PinsMapsScope.Find((pms) => pms.GetHashCode() == PinsManyMaps.GetEquivalent(pins).GetHashCode());

            if (_pinMap != null)
                _pinMap.UpdateMaps();
        }
        #endregion

        #region ---Static Helpers---
        private static void AddMapToPinsCollection(BindableMap map, UniqueObservableCollection<Pin> pins)
        {
            PinsManyMaps _pinMap = PinsMapsScope.Find((pms) => pms.GetHashCode() == PinsManyMaps.GetEquivalent(pins).GetHashCode());


            if (_pinMap != null)
            {
                _pinMap = PinsMapsScope.Find((pms) => pms == PinsManyMaps.GetEquivalent(pins));
                if (_pinMap != null)
                    _pinMap.Maps.Add(map);
            }
            else
            {

                _pinMap = new PinsManyMaps(pins, map);
                PinsMapsScope.Add(_pinMap);

                pins.CollectionChanged += PinsSourceOnCollectionChanged;

                _pinMap.UpdateMaps();
            }
        }
        #endregion
    }
}
