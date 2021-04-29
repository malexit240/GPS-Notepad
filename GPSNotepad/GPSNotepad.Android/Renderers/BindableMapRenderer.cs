using Android.Content;
using Android.Gms.Maps;
using GPSNotepad.Controls;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering.Android;
using static Android.Gms.Maps.GoogleMap;


namespace GPSNotepad.Droid
{
    public class BindableMapRenderer : ClusteredMapRenderer
    {
        #region ---Constructors---
        public BindableMapRenderer(Context context) : base(context) { }
        #endregion

        #region ---Overrides---
        protected override void OnMapReady(GoogleMap nativeMap, Map map)
        {
            base.OnMapReady(nativeMap, map);

            IOnMarkerClickListener _onMarkerClickListener = new OnMarkerClickListener(nativeMap, (BindableMap)map);
            nativeMap.SetOnMarkerClickListener(_onMarkerClickListener);
        }
        #endregion
    }
}

