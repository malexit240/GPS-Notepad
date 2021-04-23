using Foundation;
using Microsoft.Extensions.DependencyInjection;
using ObjCRuntime;
using Prism;
using Prism.Ioc;
using Shiny;
using Shiny.Jobs;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration;

//namespace GPSNotepad.iOS
//{
//    // The UIApplicationDelegate for the application. This class is responsible for launching the 
//    // User Interface of the application, as well as listening (and optionally responding) to 
//    // application events from iOS.
//    [Register("AppDelegate")]
//    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
//    {
//        //
//        // This method is invoked when the application has loaded and is ready to run. In this 
//        // method you should instantiate the window, load the UI into it and then make the window
//        // visible.
//        //
//        // You have 17 seconds to return from this method, or iOS will terminate your application.
//        //
//        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
//        {
//            global::Xamarin.Forms.Forms.Init();
//            Xamarin.FormsGoogleMaps.Init("AIzaSyD175Ytn104FswHwZ_W9GdaYHZ8OCVn_Ik");
//            LoadApplication(new App(new iOSInitializer()));
//            iOSShinyHost.Init(new YourShinyStartup());
//
//            return base.FinishedLaunching(app, options);
//        }
//
//        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
//        {
//            base.PerformFetch(application, completionHandler);
//            Shiny.Jobs.JobManager.OnBackgroundFetch(completionHandler);
//        }
//    }
//
//    public class iOSInitializer : IPlatformInitializer
//    {
//        public void RegisterTypes(IContainerRegistry containerRegistry)
//        {
//
//        }
//    }
//
//    public class YourShinyStartup : ShinyStartup
//    {
//        public override void ConfigureServices(IServiceCollection builder)
//        {
//            builder.UseNotifications();
//            builder.RegisterJob(new JobInfo(typeof(GPSNotepad.NotificationJob)) { RunOnForeground = true });
//            builder.UseJobForegroundService(TimeSpan.FromMinutes(1));
//        }
//    }
//}


namespace GPSNotepad.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // this needs to be loaded before EVERYTHING
            this.ShinyFinishedLaunching(new YourShinyStartup());

            Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD175Ytn104FswHwZ_W9GdaYHZ8OCVn_Ik");
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }


        //#if !PRODUCTION
        //// these are generated in the main sample
        //// These methods will automatically be created and wired for you, as long as
        //// 1. You install Shiny.Core in this project
        //// 2. You don't customize them (meaning you don't implement these yourself)
        //// 3. This class is marked as partial

        //public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        //    => this.ShinyDidReceiveRemoteNotification(userInfo, null);

        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //    => this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);

        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //    => this.ShinyRegisteredForRemoteNotifications(deviceToken);

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //    => this.ShinyFailedToRegisterForRemoteNotifications(error);

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyPerformFetch(completionHandler);

        //public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
        //    => this.ShinyHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);

        //#endif
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