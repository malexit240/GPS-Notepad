using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Frame), typeof(GPSNotepad.iOS.CustomFrameRenderer))]
namespace GPSNotepad.iOS
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element != null)
            {
                Element.HasShadow = false;
            }
        }
    }
}