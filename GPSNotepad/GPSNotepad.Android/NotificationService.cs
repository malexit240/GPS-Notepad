using System;
using Android.App;
using Android.Util;
using Android.Content;
using Android.OS;
using System.Threading;
using Android.Runtime;
using Android.Support.V4.App;
using Plugin.CurrentActivity;

namespace GPSNotepad.Droid.Services
{
    public static class NotificationServiceConstants
    {
        public static int SERVICE_RUNNING_NOTIFICATION_ID { get; set; } = 65228;
        public static string ACTION_START_SERVICE { get; set; } = "GPSNotepad.action.START_SERVICE";
        public static string ACTION_ADD_NOTIFICATION_INFO { get; set; } = "GPSNotepad.action.ADD_NOTIFICATION_INFO";
        public static string ACTION_STOP_SERVICE { get; set; } = "GPSNotepad.action.STOP_SERVICE";
        public static string NOTIFICATION_BROADCAST_ACTION { get; set; } = "GPSNotepad.Notification.Action";

        public static string BROADCAST_MESSAGE_KEY { get; set; } = "broadcast_message";
        public static int DELAY_BETWEEN_LOG_MESSAGES { get; set; } = 5000; // milliseconds
    }

    [Service]
    public class NotificationService : Service
    {
        static readonly string TAG = typeof(NotificationService).FullName;

        //UtcTimestamper timestamper;
        bool isStarted { get; set; }
        Handler Handler { get; set; }
        Action Runable { get; set; }

        string Data { get; set; } = "";

        public override void OnCreate()
        {

            Handler = Handler.CreateAsync(Looper.MainLooper);
            var counter = 0;
            Runable = new Action(() =>
            {
                PushNotification(counter.ToString());
                counter++;
                // RegisterForegroundService();
                Handler.PostDelayed(Runable, NotificationServiceConstants.DELAY_BETWEEN_LOG_MESSAGES);
            });

        }


        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            var data = intent.DataString;
            if (intent.Action.Equals(NotificationServiceConstants.ACTION_START_SERVICE))
            {
                if (!isStarted)
                {
                    RegisterForegroundService();
                    Handler.PostDelayed(Runable, 5000);
                    isStarted = true;
                }
            }
            else if (intent.Action.Equals(NotificationServiceConstants.ACTION_STOP_SERVICE))
            {
                StopForeground(true);
                StopSelf();
                isStarted = false;
            }

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent) => null;
        private void PushNotification(string text)
        {
            NotificationCompat.Builder builder = new NotificationCompat.Builder(CrossCurrentActivity.Current?.Activity, "2113")
.SetContentTitle(text)
.SetContentText(text).SetSmallIcon(Resource.Drawable.icon_settings).SetAutoCancel(true);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager = CrossCurrentActivity.Current?.Activity.
                GetSystemService("notification") as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        private void RegisterForegroundService()
        {
            var text = "!!!";
            NotificationCompat.Builder builder = new NotificationCompat.Builder(CrossCurrentActivity.Current?.Activity, "2113").SetContentTitle(text).SetContentText(text).SetSmallIcon(Resource.Drawable.icon_settings).SetAutoCancel(true);

            // Build the notification:
            Notification notification = builder.Build();
            StartForeground(NotificationServiceConstants.SERVICE_RUNNING_NOTIFICATION_ID, notification);
        }
    }
}