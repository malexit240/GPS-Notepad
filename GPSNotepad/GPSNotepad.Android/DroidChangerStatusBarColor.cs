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
using Android.Support.V4.App;

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