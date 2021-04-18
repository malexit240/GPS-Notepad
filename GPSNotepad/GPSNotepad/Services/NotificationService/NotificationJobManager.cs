using Xamarin.Forms;
using Prism;
using GPSNotepad.Services.PinService;
using GPSNotepad.Enums;

namespace GPSNotepad
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
        public void Reload()
        {
            NotificationJob.ReloadShedule();
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
