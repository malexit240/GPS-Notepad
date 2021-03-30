using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GPSNotepad.PlatformDependencyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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