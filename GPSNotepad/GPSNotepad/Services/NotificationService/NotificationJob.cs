using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;

namespace GPSNotepad
{

    public class NotificationJob : IJob
    {
        #region ---Public Static Fields---

        public static List<FutureNotification> NotificationsShedulde = null;

        #endregion

        #region ---Protected Static Properties---

        protected static INotificationManager NotificationManager { get; set; } = null;

        protected static IJobManager JobManager { get; set; }

        protected static Notification Notification { get; set; }

        protected static bool IsInitialized { get; set; } = false;

        #endregion

        #region ---Public Static Methods---

        public static void ReloadShedule()
        {
            NotificationsShedulde = new List<FutureNotification>();

            var pins = NotificationLoader.GetAllPins();
            var currentTime = DateTime.Now;

            foreach (var pin in pins)
            {
                var events = (from e in pin.Events where e.Time >= currentTime select e).ToList();

                foreach (var @event in events)
                {
                    NotificationsShedulde.Add(FutureNotification.Create(pin.Name, @event.Description, @event.Time));
                }
            }
            NotificationsShedulde.Sort(new FutureNotification.Comparer());
        }

        #endregion

        #region ---IJob Implementation---

        public async Task Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            if (IsInitialized == false)
                if (OnStart() != true)
                    return;

            var currentTime = DateTime.Now;

            var info = new JobInfo(typeof(NotificationJob))
            {
                RunOnForeground = true
            };



            if (NotificationsShedulde.Count != 0)
            {
                if (currentTime >= NotificationsShedulde[0].TimeToNotify)
                {
                    FireNotification(NotificationsShedulde[0]);
                    NotificationsShedulde.RemoveAt(0);
                }
            }

            await JobManager.Register(info);
        }

        #endregion

        #region ---Private Helpers---

        private async void FireNotification(FutureNotification futureNotification)
        {
            Notification.Title = "GPS Notepad";
            Notification.Message = futureNotification.NotificationText;
            Notification.Id = futureNotification.Id;
            futureNotification.IsFired = true;

            await NotificationManager.Send(Notification);
        }

        private bool OnStart()
        {
            bool result = true;
            IsInitialized = true;

            NotificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();
            JobManager = ShinyHost.Resolve<IJobManager>();

            Notification = new Notification();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Notification.Android.Category = "8976";
            }

            ReloadShedule();

            return result;
        }

        #endregion
    }
}
