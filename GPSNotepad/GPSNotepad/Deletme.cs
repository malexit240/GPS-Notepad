using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPSNotepad
{
    public class NotificationJob : IJob
    {
        public static List<DateTime> dateTimes = new List<DateTime>()
            {
                DateTime.Now.AddSeconds(10),
                DateTime.Now.AddSeconds(15),
                DateTime.Now.AddSeconds(20),
                DateTime.Now.AddSeconds(30),
                DateTime.Now.AddSeconds(60),
                DateTime.Now.AddSeconds(120),
            };

        public static int counter = 0;
        public static INotificationManager manager { get; set; } = null;
        public static Notification Notification { get; set; }
        public static IJobManager jobManager { get; set; }

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            var time = dateTimes[counter];

            manager = manager ?? Shiny.ShinyHost.Resolve<Shiny.Notifications.INotificationManager>();
            Notification = Notification ?? new Notification()
            {
                Title = "*",
                Message = "*",
                Id = new Random().Next()
            };

            Notification.Android.ChannelId = "8976";

            jobManager = jobManager ?? ShinyJobManager.Current;

            if (DateTime.Now < time)
            {
                var info = new JobInfo(typeof(NotificationJob));
                info.RunOnForeground = true;

                await jobManager.Schedule(info);
                return true;
            }

            Notification.Title = counter.ToString();
            Notification.Id = new Random().Next();

            await manager.Send(Notification);

            counter++;
            if (dateTimes.Count > counter)
            {
                var info = new JobInfo(typeof(NotificationJob));
                info.RunOnForeground = true;

                await jobManager.Schedule(info);
            }

            return true;
        }


    }
}
