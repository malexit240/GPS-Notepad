using Xamarin.Forms;
using Prism;
using GPSNotepad.Services.PinService;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Enums;
using Xamarin.Essentials;
using System.Linq;
using System.Collections.Generic;
using System;

namespace GPSNotepad.Services.NotificationService
{
    public class NotificationJobManager
    {
        #region ---Constructors---
        public NotificationJobManager()
        {
            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);
        }
        #endregion

        #region ---Public Methods---
        public async void Reload()
        {

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                NotificationJob.ReloadShedule();
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var id = App.Current.Container.Resolve<IAuthorizationService>().GetCurrenUserId();
                var pins = await App.Current.Container.Resolve<IPinService>().GetAllPinsForUser(id);

                var now = DateTime.Now;

                var notification = new List<FutureNotification>();
                foreach (var pin in pins)
                {
                    foreach (var @event in pin.Events.Select(e => e).Where(e => e.Time >= DateTime.Now))
                    {
                        notification.Add(FutureNotification.Create(pin.Name, @event.Description, @event.Time));
                    }

                    notification.Sort(new FutureNotification.Comparer());
                }

                DependencyService.Get<ILocalNotificationManager>().ScheduleLocalNotifications(notification);
            }

        }
        #endregion

        #region ---Event Handlers---
        private void OnPinStateChanged(PrismApplicationBase app, PinsStateChangedMessage message)
        {
            switch (message.ChangedType)
            {
                case PinsStateChangedType.Add:
                case PinsStateChangedType.UpdateEvents:
                case PinsStateChangedType.Delete:
                    this.Reload();
                    break;
            }
        }
        #endregion
    }
}
