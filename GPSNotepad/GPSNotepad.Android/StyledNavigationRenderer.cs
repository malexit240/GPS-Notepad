﻿using Android.Content;
using GPSNotepad.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace GPSNotepad.Droid
{
    public class StyledNavigationRenderer : NavigationPageRenderer
    {
        public StyledNavigationRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            e.NewElement.Style = App.Current.Resources["pageStyle"] as Style;
        }
    }
}

