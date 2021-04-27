using GPSNotepad.PlatformDependencyInterfaces;
using Xamarin.Forms;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(GPSNotepad.Droid.DroidChangerStatusBarColor))]

namespace GPSNotepad.Droid
{

    class DroidChangerStatusBarColor : IChangerBarColor
    {
        #region ---Public Methods---
        public void SetBarColor(Color color)
        {
            var newColor = Android.Graphics.Color.ParseColor(color.ToHex());

            CrossCurrentActivity.Current?.Activity?.Window?.SetStatusBarColor(newColor);
            CrossCurrentActivity.Current?.Activity?.Window?.SetNavigationBarColor(newColor);
        }
        #endregion

    }
}