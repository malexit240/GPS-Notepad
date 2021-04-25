using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;
using System;
using GPSNotepad.Extensions;
using System.Collections.ObjectModel;

namespace GPSNotepad.Controls
{
    public class BindableMap : ClusteredMap
    {
        #region ---Private Fields---
        private bool _isMapLoading;

        private bool _isIdle;
        #endregion

        #region ---Constructors---
        public BindableMap()
        {
            _isMapLoading = true;
            ShowInfoWindow = false;
            _isIdle = true;

            this.CameraMoveStarted += OnCameraMoveStarted;
            this.CameraIdled += OnCameraIdled;
            this.CameraMoving += OnCameraMoving;
        }
        #endregion

        #region ---Destructors---
        ~BindableMap()
        {
            this.CameraMoveStarted -= OnCameraMoveStarted;
            this.CameraIdled -= OnCameraIdled;
            this.CameraMoving -= OnCameraMoving;
        }
        #endregion

        #region ---Source Properties---
        public ObservableCollection<PinViewModel> PinsSource
        {
            get => (ObservableCollection<PinViewModel>)GetValue(PinsSourceProperty);
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

        #region ---Events---
        public event EventHandler MapLoaded;

        public event EventHandler<PinTappedEventArgs> ShowDetaiPinView;
        #endregion

        #region ---Public Methods---
        public void RaiseShowDetaiPinView(Pin pin)
        {
            ShowDetaiPinView?.Invoke(this, new PinTappedEventArgs(pin));
        }

        #endregion

        #region ---Bindable Source Properties---
        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
                                                         propertyName: nameof(PinsSource),
                                                         returnType: typeof(ObservableCollection<PinViewModel>),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: nameof(MapSpan),
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: MapSpanPropertyChanged);

        public static readonly BindableProperty ShowInfoWindowProperty = BindableProperty.Create(
                                                         propertyName: nameof(ShowInfoWindow),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(BindableMap),
                                                         defaultBindingMode: BindingMode.TwoWay);

        #endregion

        #region ---Event Handlers---
        private void OnCameraIdled(object sender, CameraIdledEventArgs e)
        {
            _isIdle = true;

            if (_isMapLoading == true)
            {
                _isMapLoading = false;
                MapLoaded?.Invoke(this, new EventArgs());
            }
        }

        private void OnCameraMoveStarted(object sender, CameraMoveStartedEventArgs e) => _isIdle = false;

        private void OnCameraMoving(object sender, CameraMovingEventArgs e)
        {
            this.MapSpan = new MapSpan(e.Position.Target,
                this.MapSpan?.LatitudeDegrees ?? 0.01,
                this.MapSpan?.LongitudeDegrees ?? 0.01);
        }

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            if (map._isIdle == true)
            {
                map?.MoveToRegion(newMapSpan);
            }
        }

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var map = bindable as BindableMap;
            var newPinsSource = newValue as ObservableCollection<PinViewModel>;

            if (map != null && newPinsSource != null)
            {
                newPinsSource.CollectionChanged += map.PinsSourceOnCollectionChanged;
                map.UpdatePinsSource(newPinsSource);
            }
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdatePinsSource(PinsSource);
        }
        #endregion

        #region ---Private Helpers---
        private void UpdatePinsSource(ObservableCollection<PinViewModel> newSource)
        {
            Pins.Clear();

            foreach (var pin in newSource)
            {
                var pinView = pin.GetGoogleMapsPin();
                pinView.Type = PinType.Place;

                if (ShowInfoWindow == false)
                {
                    pinView.Label = pin.PinId.ToString();
                }

                pinView.Icon = pin.Favorite == true ?
                    BitmapDescriptorFactory.FromBundle("purpleMarkerSmallWithCrown.png") : //Favorite
                    BitmapDescriptorFactory.FromBundle("green_grayMarkerSmall.png"); //Usual

                Pins.Add(pinView);
            }

            this.Cluster();
        }
        #endregion
    }
}
