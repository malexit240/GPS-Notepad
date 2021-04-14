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

namespace GPSNotepad
{
    public class NotificationJob : IJob
    {
        public static List<FutureNotification> NotificationsShedulde = new List<FutureNotification>();

        public static int counter = 0;
        public static INotificationManager notificationManager { get; set; } = null;
        public static IJobManager jobManager { get; set; }

        public static Notification Notification { get; set; }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            notificationManager = notificationManager ?? Shiny.ShinyHost.Resolve<INotificationManager>();
            var pins = new List<Pin>();
            using (var context = new Context())
            {
                pins = context.Pins.Select(p => p).ToList();
            }
            jobManager = jobManager ?? ShinyJobManager.Current;

            Notification = Notification ?? new Notification()
            {
                Title = "*",
                Message = "*",
                Id = new Random().Next()
            };

            Notification.Android.ChannelId = "8976";

            var info = new JobInfo(typeof(NotificationJob));
            info.RunOnForeground = true;

            // var currentTime = DateTime.Now;

            //if (NotificationsShedulde.Count == 0)
            //{
            //    await jobManager.Schedule(info);
            //    return true;
            //}

            //if (currentTime < NotificationsShedulde[0].TimeToNotify)
            //{
            //    await jobManager.Schedule(info);
            //}
            //else
            //{
            //FireNotification(NotificationsShedulde[0]);

            //Notification.Title = "Notification";
            //Notification.Message = NotificationsShedulde[0].NotificationText;
            //Notification.Id = NotificationsShedulde[0].Id;
            //NotificationsShedulde[0].IsFired = true;

            try
            {
                Notification.Message = pins.Count.ToString();
            }
            catch (Exception e)
            {
                Notification.Message = e.Message;
            }

            // await notificationManager.Send(Notification);

            // NotificationsShedulde = NotificationsShedulde.Select(n => n).Where(n => !n.IsFired).ToList();

         //   await jobManager.Schedule(info);
            //}

            return true;
        }

        private async void FireNotification(FutureNotification futureNotification)
        {
            Notification.Title = "Notification";
            Notification.Message = futureNotification.NotificationText;
            Notification.Id = futureNotification.Id;
            futureNotification.IsFired = true;

            await notificationManager.Send(Notification);
        }


    }
}
