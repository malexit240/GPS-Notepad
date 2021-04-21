using Foundation;
using Microsoft.Extensions.DependencyInjection;
using ObjCRuntime;
using Prism;
using Prism.Ioc;
using Shiny;
using Shiny.Jobs;
using System;
using UIKit;
using Xamarin.Forms.PlatformConfiguration;

namespace GPSNotepad.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD175Ytn104FswHwZ_W9GdaYHZ8OCVn_Ik");
            LoadApplication(new App(new iOSInitializer()));
            iOSShinyHost.Init(new YourShinyStartup());

            return base.FinishedLaunching(app, options);
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            base.PerformFetch(application, completionHandler);
            Shiny.Jobs.JobManager.OnBackgroundFetch(completionHandler);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }

    public class YourShinyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection builder)
        {
            builder.UseNotifications();
            builder.RegisterJob(new JobInfo(typeof(GPSNotepad.NotificationJob)) { RunOnForeground = true });
            builder.UseJobForegroundService(TimeSpan.FromMinutes(1));
        }
    }
}
