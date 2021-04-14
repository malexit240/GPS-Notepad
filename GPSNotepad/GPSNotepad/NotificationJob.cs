using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using GPSNotepad.Services.PinService;
using GPSNotepad.Model.Entities;
using GPSNotepad.Repositories;
using Xamarin.Essentials;
using Microsoft.EntityFrameworkCore;

namespace GPSNotepad
{
    public class NotificationJob : IJob
    {
        public static List<FutureNotification> NotificationsShedulde = null;

        protected static INotificationManager notificationManager { get; set; } = null;
        protected static IJobManager jobManager { get; set; }
        protected static Notification Notification { get; set; }

        protected static bool IsInitialize { get; set; } = false;

        protected bool OnStart()
        {
            bool result = true;
            IsInitialize = true;

            notificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();
            jobManager = ShinyJobManager.Current;

            Notification = new Notification();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Notification.Android.ChannelId = "8976";
            }

            NotificationsShedulde = new List<FutureNotification>();

            var token = SecureStorage.GetAsync("SessionToken").Result ?? Guid.Empty.ToString();

            User user;
            List<Pin> pins;
            List<PlaceEvent> events = new List<PlaceEvent>();

            using (var context = new Context())
            {
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

            return result;
        }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            if (IsInitialize == false)
                OnStart();

            var currentTime = DateTime.Now;

            if (NotificationsShedulde.Count != 0)
            {
                if (currentTime >= NotificationsShedulde[0].TimeToNotify)
                {
                    FireNotification(NotificationsShedulde[0]);
                    NotificationsShedulde.RemoveAt(0);
                }

                var info = new JobInfo(typeof(NotificationJob));
                info.RunOnForeground = true;
                await jobManager.Schedule(info);
            }

            return true;
        }

        private async void FireNotification(FutureNotification futureNotification)
        {
            Notification.Title = "GPS Notepad";
            Notification.Message = futureNotification.NotificationText;
            Notification.Id = futureNotification.Id;
            futureNotification.IsFired = true;

            await notificationManager.Send(Notification);
        }


    }
}
