using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using Prism.Mvvm;
using Xamarin.Forms;
using System.Linq;

namespace GPSNotepad.Model
{
    public class PinsState : BindableBase
    {
        #region ---Public Static Properties---
        private static PinsState _instance;
        public static PinsState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PinsState();
                return _instance;
            }
        }
        #endregion

        #region ---Constructors---
        public PinsState()
        {
            _pins = new List<Pin>();
            Intiialize();
        }
        #endregion

        #region ---Public Properties---
        private List<Pin> _pins;

        public List<Pin> Pins
        {
            get => _pins;

            set => SetProperty(ref _pins, value);
        }
        #endregion

        #region ---Public Methods---
        public async void Intiialize()
        {
            await App.Current.Container.Resolve<IPermanentPinService>().GetAllPinsForUser(CurrentUser.Instance.UserId).ContinueWith((result) =>
            {
                Pins = result.Result;
                MessagingCenter.Send(App.Current, "pins_state_changed", new PinsStateChangedMessage(result.Result, PinsStateChangedType.Add));
            });
        }

        public bool ContainsPin(System.Guid pinId)
        {
            return _pins.Any(p => p.PinId == pinId);
        }

        public void Create(Pin pin)
        {
            Pins.Add(pin);

            App.Current.Container.Resolve<IPermanentPinService>().CreatePin(pin);

            MessagingCenter.Send(App.Current, "pins_state_changed",
                new PinsStateChangedMessage(new List<Pin>() { pin },
                PinsStateChangedType.Add));

        }

        public void Update(Pin pin)
        {
            var index = Pins.IndexOf(pin);

            if (index == -1)
                return;

            Pins.RemoveAt(index);
            Pins.Insert(index, pin);

            App.Current.Container.Resolve<IPermanentPinService>().UpdatePin(pin);

            MessagingCenter.Send(App.Current, "pins_state_changed",
                new PinsStateChangedMessage(new List<Pin>() { pin },
                PinsStateChangedType.Update));
        }

        public void Delete(Pin pin)
        {
            Pins.Remove(pin);
            App.Current.Container.Resolve<IPermanentPinService>().DeletePin(pin);

            MessagingCenter.Send(App.Current, "pins_state_changed",
                new PinsStateChangedMessage(new List<Pin>() { pin },
                PinsStateChangedType.Delete));
        }
        #endregion
    }
}
