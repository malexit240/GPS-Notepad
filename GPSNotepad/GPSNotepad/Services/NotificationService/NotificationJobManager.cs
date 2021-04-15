using Xamarin.Forms;
using GPSNotepad.Model;
using Prism;

namespace GPSNotepad
{
    public class NotificationJobManager
    {
        public NotificationJobManager()
        {
            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);
        }

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

        public void Reload()
        {
            NotificationJob.ReloadShedule();
        }
    }
}
