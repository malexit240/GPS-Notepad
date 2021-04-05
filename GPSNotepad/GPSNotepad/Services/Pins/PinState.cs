using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using Prism.Mvvm;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public class PinState : BindableBase
    {
        #region ---Public Static Properties---
        private static PinState _instance;
        public static PinState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PinState();
                return _instance;
            }
        }

        private bool _isLoaded = false;
        public bool IsLoaded => _isLoaded;

        public void LoadPins(List<Pin> pins)
        {
            _isLoaded = true;

            Pins = pins;

            MessagingCenter.Send(App.Current, "pins_state_changed", new PinsStateChangedMessage(pins, PinsStateChangedType.Load));
        }

        #endregion

        #region ---Constructors---
        public PinState()
        {
            _pins = new List<Pin>();
            //Intiialize();
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

        public bool ContainsPin(System.Guid pinId)
        {
            return _pins.Any(p => p.PinId == pinId);
        }

        public void Create(Pin pin)
        {
            Pins.Add(pin);

            MessagingCenter.Send(App.Current, "pins_state_changed",
                new PinsStateChangedMessage(new List<Pin>() { pin },
                PinsStateChangedType.Add));

        }

        public void Update(Pin pin)
        {
            var index = Pins.IndexOf(pin);

            if (index != -1)
            {
                Pins.RemoveAt(index);
                Pins.Insert(index, pin);

                MessagingCenter.Send(App.Current, "pins_state_changed",
                    new PinsStateChangedMessage(new List<Pin>() { pin },
                    PinsStateChangedType.Update));
            }
        }

        public void Delete(Pin pin)
        {
            if (Pins.Remove(pin))
            {
                MessagingCenter.Send(App.Current, "pins_state_changed",
                    new PinsStateChangedMessage(new List<Pin>() { pin },
                    PinsStateChangedType.Delete));
            }
        }
        #endregion
    }
}
