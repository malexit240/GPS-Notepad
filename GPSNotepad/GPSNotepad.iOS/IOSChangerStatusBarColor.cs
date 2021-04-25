using Foundation;
using GPSNotepad.PlatformDependencyInterfaces;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(GPSNotepad.iOS.IOSChangerStatusBarColor))]
namespace GPSNotepad.iOS
{
    public class IOSChangerStatusBarColor : IChangerBarColor
    {
        public void SetBarColor(Color color)
        {
            //UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            //
            //if (statusBar != null && statusBar.RespondsToSelector(
            //new ObjCRuntime.Selector("setBackgroundColor:")))
            //{
            //
            //    statusBar.BackgroundColor = color.ToUIColor();
            //}
        }
    }
}
