using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android;
using Xamarin.Forms.Platform.Android;
using static Android.Gms.Maps.GoogleMap;

[assembly: ExportRenderer(typeof(GPSNotepad.BindableMap), typeof(GPSNotepad.Droid.BindableMapRenderer))]
namespace GPSNotepad.Droid
{
    public class OnMarkerClickListener : Java.Lang.Object, IOnMarkerClickListener
    {
        GoogleMap NativeMap { get; set; }
        BindableMap Map { get; set; }

        public OnMarkerClickListener(GoogleMap nativeMap, BindableMap map) : base()
        {
            NativeMap = nativeMap;
            Map = map;
        }

        public bool OnMarkerClick(Marker marker)
        {
            if (Map.ShowInfoWindow)
                return false;

            Map.RaiseShowDetaiPinView(new Pin()
            {
                Label = marker.Title
            });

            return true;
        }
    }


    public class BindableMapRenderer : MapRenderer
    {

        private IOnMarkerClickListener _onMarkerClickListener;

        public BindableMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnMapReady(GoogleMap nativeMap, Map map)
        {
            base.OnMapReady(nativeMap, map);

            IOnMarkerClickListener _onMarkerClickListener = new OnMarkerClickListener(nativeMap, (BindableMap)map);
            nativeMap.SetOnMarkerClickListener(_onMarkerClickListener);

        }
    }
}

