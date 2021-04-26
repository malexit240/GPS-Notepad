using Shiny.Notifications;
using System;
using Xamarin.Essentials;

namespace GPSNotepad
{
    public class Loger
    {
        #region ---Public Static Properties---
        private static Loger _instance = null;
        public static Loger Instance => _instance ??= new Loger();
        #endregion

        #region ---Constructors---
        private Loger()
        {
            NotificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();

            Notification = new Notification();
            //Notification.Channel = Channel.Create();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Notification.Android.Category = "8977";

            }
        }
        #endregion

        #region ---Protected Fields---
        protected INotificationManager NotificationManager;
        protected Notification Notification;
        #endregion

        #region ---Public Methods---
        public async void LogAsync(string message)
        {
            Notification.Title = "Loger";
            Notification.Message = message;
            Notification.Id = new Random().Next();

            await NotificationManager.Send(Notification);
        }
        #endregion

    }
}
