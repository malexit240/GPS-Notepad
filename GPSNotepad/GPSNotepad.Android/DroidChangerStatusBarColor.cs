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
        public void SetBarColor(Color color)
        {
            CrossCurrentActivity.Current?.Activity?.Window?.SetStatusBarColor(Android.Graphics.Color.ParseColor(color.ToHex()));
            CrossCurrentActivity.Current?.Activity?.Window?.SetNavigationBarColor(Android.Graphics.Color.ParseColor(color.ToHex()));

            //PushNotification("Theme changed!");

        }

        private void PushNotification(string text)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(CrossCurrentActivity.Current?.Activity, "2113")
.SetContentTitle(text)
.SetContentText(text).SetSmallIcon(Resource.Drawable.icon_settings);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager = CrossCurrentActivity.Current?.Activity.
                GetSystemService("notification") as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}