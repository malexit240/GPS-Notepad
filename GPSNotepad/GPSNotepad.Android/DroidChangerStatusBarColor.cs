using GPSNotepad.PlatformDependencyInterfaces;
using Xamarin.Forms;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(GPSNotepad.Droid.DroidChangerStatusBarColor))]

namespace GPSNotepad.Droid
{

    class DroidChangerStatusBarColor : IChangerBarColor
    {
        public void SetBarColor(Color color)
        {
            CrossCurrentActivity.Current?.Activity?.Window?.SetStatusBarColor(Android.Graphics.Color.ParseColor(color.ToHex()));
            CrossCurrentActivity.Current?.Activity?.Window?.SetNavigationBarColor(Android.Graphics.Color.ParseColor(color.ToHex()));
        }
    }
}