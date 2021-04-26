using Foundation;
using Microsoft.Extensions.DependencyInjection;
using ObjCRuntime;
using Prism;
using Prism.Ioc;
using Shiny;
using Shiny.Jobs;
using System;
using UIKit;
using UserNotifications;

namespace GPSNotepad.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD175Ytn104FswHwZ_W9GdaYHZ8OCVn_Ik");

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            // show an alert
            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);

            // reset our badge
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> compvarionHandler)
        {
            base.PerformFetch(application, compvarionHandler);
        }
    }


}