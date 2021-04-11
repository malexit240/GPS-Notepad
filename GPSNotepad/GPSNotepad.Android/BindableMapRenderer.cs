using Android.Content;
using Android.Gms.Maps;
using GPSNotepad.Controls;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android;
using Xamarin.Forms.GoogleMaps.Clustering.Android;
using Xamarin.Forms.Platform.Android;
using static Android.Gms.Maps.GoogleMap;

[assembly: ExportRenderer(typeof(GPSNotepad.Controls.BindableMap), typeof(GPSNotepad.Droid.BindableMapRenderer))]
namespace GPSNotepad.Droid
{
    public class BindableMapRenderer : ClusteredMapRenderer
    {
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

