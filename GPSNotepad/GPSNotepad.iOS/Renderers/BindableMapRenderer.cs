using Google.Maps;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GPSNotepad.Controls.BindableMap), typeof(GPSNotepad.iOS.BindableMapRenderer))]
namespace GPSNotepad.iOS
{
    public class BindableMapRenderer : ClusteredMapRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                NativeMap.TappedMarker += OnmarkerTapped;
            }
        }

        private bool OnmarkerTapped(MapView mapView, Marker marker)
        {
            var map = (GPSNotepad.Controls.BindableMap)Map;
            bool result = false;
            if (!map.ShowInfoWindow)
            {
                map.RaiseShowDetaiPinView(new Pin()
                {
                    Label = marker.Title
                });
                result = true;
            }

            return result;
        }
    }
}