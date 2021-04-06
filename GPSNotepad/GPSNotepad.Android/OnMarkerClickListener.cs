using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using GPSNotepad.Controls;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using static Android.Gms.Maps.GoogleMap;

[assembly: ExportRenderer(typeof(GPSNotepad.Controls.BindableMap), typeof(GPSNotepad.Droid.BindableMapRenderer))]
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
}

