using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;
using System;
using GPSNotepad.Extensions;

namespace GPSNotepad.Controls
{
    public class BindableMap : ClusteredMap
    {
        public BindableMap()
        {
            IsMapLoading = true;
            ShowInfoWindow = false;
            this.CameraMoveStarted += OnCameraMoveStarted;
            this.CameraIdled += OnCameraIdled;
            this.CameraMoving += OnCameraMoving;
        }

        public event EventHandler MapLoaded;

        public event EventHandler<PinTappedEventArgs> ShowDetaiPinView;

        public void RaiseShowDetaiPinView(Pin pin)
        {
            ShowDetaiPinView?.Invoke(this, new PinTappedEventArgs(pin));
        }

        private bool IsMapLoading { get; set; }

        public bool IsIdle { get; private set; } = true;

        private void OnCameraIdled(object sender, CameraIdledEventArgs e)
        {
            IsIdle = true;

            if (IsMapLoading)
            {
                IsMapLoading = false;
                MapLoaded?.Invoke(this, new EventArgs());
            }
        }

        private void OnCameraMoveStarted(object sender, CameraMoveStartedEventArgs e) => IsIdle = false;

        private void OnCameraMoving(object sender, CameraMovingEventArgs e)
        {
            this.MapSpan = new MapSpan(e.Position.Target,
                this.MapSpan?.LatitudeDegrees ?? 0.01,
                this.MapSpan?.LongitudeDegrees ?? 0.01);
        }

        #region ---Source Properties---
        public UniqueObservableCollection<PinViewModel> PinsSource
        {
            get => (UniqueObservableCollection<PinViewModel>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public bool ShowInfoWindow
        {
            get => (bool)GetValue(ShowInfoWindowProperty);
            set => SetValue(ShowInfoWindowProperty, value);
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
                                                         returnType: typeof(UniqueObservableCollection<PinViewModel>),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: "MapSpan",
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: MapSpanPropertyChanged);

        public static readonly BindableProperty ShowInfoWindowProperty = BindableProperty.Create(
                                                         propertyName: "ShowInfoWindow",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay);


        private static readonly Dictionary<UniqueObservableCollection<PinViewModel>, BindableMap> Scope = new Dictionary<UniqueObservableCollection<PinViewModel>, BindableMap>();
        #endregion

        #region ---Event Handlers---
        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            if (map.IsIdle)
            {
                map?.MoveToRegion(newMapSpan);
            }
        }

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var map = bindable as BindableMap;
            var newPinsSource = newValue as UniqueObservableCollection<PinViewModel>;

            if (map != null && newPinsSource != null)
            {
                Scope.Add(newPinsSource, map);
                newPinsSource.CollectionChanged += PinsSourceOnCollectionChanged;

                map.UpdatePinsSource(newPinsSource);
            }
        }

        private static void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var pins = sender as UniqueObservableCollection<PinViewModel>;

            if (Scope.ContainsKey(pins))
            {
                var map = Scope[pins];

                map.UpdatePinsSource(pins);
            }
        }
        #endregion

        private void UpdatePinsSource(UniqueObservableCollection<PinViewModel> newSource)
        {
            Pins.Clear();
            foreach (var pin in newSource)
            {
                var p = pin.GetGoogleMapsPin();
                p.Type = PinType.Place;
                if (pin.Favorite == true)
                    p.Icon = BitmapDescriptorFactory.FromBundle("purpleMarkerSmallWithCrown.png");
                else
                    p.Icon = BitmapDescriptorFactory.FromBundle("green_grayMarkerSmall.png");
                if (ShowInfoWindow == false)
                    p.Label = pin.PinId.ToString();
                Pins.Add(p);
            }
            this.Cluster();
        }

    }
}
