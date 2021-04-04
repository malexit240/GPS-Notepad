using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android;
using static Android.Gms.Maps.GoogleMap;

[assembly: ExportRenderer(typeof(GPSNotepad.BindableMap), typeof(GPSNotepad.Droid.BindableMapRenderer))]
namespace GPSNotepad.Droid
{
    public class OnMarkerClickListener : Java.Lang.Object, IOnMarkerClickListener
    {

        public bool OnMarkerClick(Marker marker)
        {
            return true;
        }
    }
    public class BindableMapRenderer : MapRenderer
    {
        public BindableMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnMapReady(GoogleMap nativeMap, Map map)
        {
            base.OnMapReady(nativeMap, map);

            IOnMarkerClickListener listner = new OnMarkerClickListener();
            nativeMap.SetOnMarkerClickListener(listner);

            //nativeMap.MarkerClick += NativeMap_MarkerClick;


        }

        private void NativeMap_MarkerClick(object sender, MarkerClickEventArgs e)
        {

        }
    }
}

