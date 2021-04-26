using GPSNotepad.Services.NotificationService;
using Shiny;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GPSNotepad.iOS.iOSLocalNotificationManager))]
namespace GPSNotepad.iOS
{
    public class iOSLocalNotificationManager : ILocalNotificationManager
    {

        public void RegisterUserNotificationSettings()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );

                UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);
            }
        }

        public void ScheduleLocalNotifications(List<FutureNotification> futureNotifications)
        {
            RegisterUserNotificationSettings();

            UIApplication.SharedApplication.CancelAllLocalNotifications();
            foreach (var notification in futureNotifications)
            {
                RegisterNotification(notification);
            }
        }

        private void RegisterNotification(FutureNotification futureNotification)
        {
            var notification = new UILocalNotification();

            notification.FireDate = futureNotification.TimeToNotify.ToNSDate();

            notification.AlertAction = "View Alert";
            notification.AlertBody = futureNotification.NotificationText;
            notification.AlertTitle = futureNotification.NotificationTitle;

            notification.ApplicationIconBadgeNumber = 1;
            notification.SoundName = UILocalNotification.DefaultSoundName;

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }


    }

}