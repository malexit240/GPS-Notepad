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
        #region ---Private Fields---
        private GoogleMap NativeMap;
        private BindableMap Map;
        #endregion

        #region ---Constructors---
        public OnMarkerClickListener(GoogleMap nativeMap, BindableMap map) : base()
        {
            NativeMap = nativeMap;
            Map = map;
        }
        #endregion

        #region ---IOnMarkerClickListnerImpleentation---
        public bool OnMarkerClick(Marker marker)
        {
            bool result = false;
            if (!Map.ShowInfoWindow)
            {
                Map.RaiseShowDetaiPinView(new Pin()
                {
                    Label = marker.Title
                });
                result = true;
            }

            return result;
        }
        #endregion
    }
}

