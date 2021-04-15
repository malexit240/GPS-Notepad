using Shiny.Notifications;
using System;
using Xamarin.Essentials;

namespace GPSNotepad
{
    public class Loger
    {
        private static Loger _instance = null;
        public static Loger Instance => _instance ??= new Loger();

        protected INotificationManager notificationManager { get; set; } = null;
        protected Notification Notification { get; set; }

        public Loger()
        {
            notificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();

            Notification = new Notification();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Notification.Android.ChannelId = "8977";
            }
        }

        public async void Log(string message)
        {
            Notification.Title = "Loger";
            Notification.Message = message;
            Notification.Id = new Random().Next();

            await notificationManager.Send(Notification);
        }

    }
}
