using System.Collections.Generic;

namespace GPSNotepad.PlatformDependencyInterfaces
{
    public interface ILocalNotificationManager
    {
        void ScheduleLocalNotifications(List<FutureNotification> futureNotifications);
    }

}