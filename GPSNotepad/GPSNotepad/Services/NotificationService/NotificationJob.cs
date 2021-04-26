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
    public static class NotificationLoader
    {
        public static List<Pin> GetAllPins()
        {
            var token = Preferences.Get("SessionToken", Guid.Empty.ToString());

            User user;
            List<Pin> pins = null;
            List<PlaceEvent> events = new List<PlaceEvent>();

            using var context = new Context();

            user = context.Users.Select(u => u).Where(u => u.SessionToken == token).FirstOrDefault();

            if (user != null)
            {
                pins = context.Pins.Include(pin => pin.Events).Select(p => p).Where(p => p.UserId == user.UserId).ToList();
            }

            return pins;
        }
    }

    public class NotificationJob : IJob
    {
        public static List<FutureNotification> NotificationsShedulde = null;

        protected static INotificationManager NotificationManager { get; set; } = null;
        protected static IJobManager JobManager { get; set; }
        protected static Notification Notification { get; set; }

        protected static bool IsInitialized { get; set; } = false;

        protected bool OnStart()
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

        public static void ReloadShedule()
        {
            NotificationsShedulde = new List<FutureNotification>();

            var pins = NotificationLoader.GetAllPins();

            foreach (var pin in pins)
            {
                foreach (var @event in pin.Events.Select(e => e).Where(e => e.Time >= DateTime.Now))
                {
                    NotificationsShedulde.Add(FutureNotification.Create(pin.Name, @event.Description, @event.Time));
                }

                NotificationsShedulde.Sort(new FutureNotification.Comparer());
            }
        }

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
