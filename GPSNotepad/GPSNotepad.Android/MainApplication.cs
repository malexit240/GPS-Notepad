using System;
using Android.App;
using Android.Runtime;
using Shiny;

namespace GPSNotepad.Droid
{
    [Application(
        Theme = "@style/MainTheme"
        )]
    public class MainApplication : Application
    {
        #region ---Constructors---
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion
        #region ---Overrides---
        public override void OnCreate()
        {
            base.OnCreate();

            Shiny.ShinyHost.Init(new Shiny.AndroidPlatform(this), new GPSShinyStartup());

            Xamarin.Essentials.Platform.Init(this);
        }
        #endregion
    }
}
