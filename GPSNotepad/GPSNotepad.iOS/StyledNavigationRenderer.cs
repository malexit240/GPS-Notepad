using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GPSNotepad.iOS.StyledNavigationRenderer), typeof(NavigationPage))]
namespace GPSNotepad.iOS
{
    public class StyledNavigationRenderer : PageRenderer
    {
        #region ---Overrides---
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

        }
        #endregion
    }
}

