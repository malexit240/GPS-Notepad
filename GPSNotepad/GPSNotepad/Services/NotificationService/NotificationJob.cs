using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using GPSNotepad.Services.PinService;
using GPSNotepad.Entities;
using GPSNotepad.Repositories;
using Xamarin.Essentials;
using Microsoft.EntityFrameworkCore;

namespace GPSNotepad
{
    public class NotificationJob : IJob
    {
        public static List<FutureNotification> NotificationsShedulde = null;

        protected static INotificationManager NotificationManager { get; set; } = null;
        protected static IJobManager JobManager { get; set; }
        protected static Notification Notification { get; set; }

        protected static bool IsInitialize { get; set; } = false;

        protected bool OnStart()
        {
            bool result = true;
            IsInitialize = true;

            NotificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();
            JobManager = ShinyJobManager.Current;

            Notification = new Notification();

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Notification.Android.ChannelId = "8976";
            }

            ReloadShedule();

            return result;
        }

        public static void ReloadShedule()
        {
            NotificationsShedulde = new List<FutureNotification>();

            var token = SecureStorage.GetAsync("SessionToken").Result ?? Guid.Empty.ToString();

            User user;
            List<Pin> pins;
            List<PlaceEvent> events = new List<PlaceEvent>();

            using var context = new Context();

            user = context.Users.Select(u => u).Where(u => u.SessionToken == token).FirstOrDefault();
            if (user != null)
            {
                pins = context.Pins.Include(pin => pin.Events).Select(p => p).Where(p => p.UserId == user.UserId).ToList();

                foreach (var pin in pins)
                {
                    foreach (var @event in pin.Events.Select(e => e).Where(e => e.Time >= DateTime.Now))
                    {
                        FutureNotification.Create(pin.Name, @event.Description, @event.Time);
                    }
                }
            }
        }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            if (IsInitialize == false)
                if (OnStart() != true)
                    return true;

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

            await JobManager.Schedule(info);

            return true;
        }

        private async void FireNotification(FutureNotification futureNotification)
        {
            Notification.Title = "GPS Notepad";
            Notification.Message = futureNotification.NotificationText;
            Notification.Id = futureNotification.Id;
            futureNotification.IsFired = true;

            await NotificationManager.Send(Notification);
        }
    }
}
