using System.Collections.Generic;

namespace GPSNotepad.Services.NotificationService
{
    public interface ILocalNotificationManager
    {
        void RegisterUserNotificationSettings();
        void ScheduleLocalNotifications(List<FutureNotification> futureNotifications);
    }

}