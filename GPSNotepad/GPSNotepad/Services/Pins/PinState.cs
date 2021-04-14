using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using Prism.Mvvm;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GPSNotepad.Model
{
    public class PinState : BindableBase
    {
        #region ---Public Static Properties---
        private static PinState _instance;
        public static PinState Instance => _instance ??= new PinState();


        private bool _isLoaded = false;
        public bool IsLoaded => _isLoaded;

        public void LoadPins(List<Pin> pins)
        {
            _isLoaded = true;

            Pins = pins;

            MessagingCenter.Send(App.Current, "pins_state_changed", new PinsStateChangedMessage(pins, PinsStateChangedType.Load));
        }

        internal void DeleteEvent(Guid pinId, PlaceEvent placeEvent)
        {
            var pin = Pins.Select(p => p).Where(p => p.PinId == pinId).FirstOrDefault();

            if (pin != null)
            {
                var index = pin.Events.IndexOf(placeEvent);

                if (index != -1)
                {
                    pin.Events.RemoveAt(index);
                    MessagingCenter.Send(App.Current, "pins_state_changed",
                        new PinsStateChangedMessage(new List<Pin>() { pin },
                        PinsStateChangedType.UpdateEvents));
                }
            }
        }

        #endregion

        #region ---Constructors---
        public PinState()
        {
            _pins = new List<Pin>();
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

        public void UpdateEvent(Guid pinId, PlaceEvent @event)
        {
            var pin = Pins.Select(p => p).Where(p => p.PinId == pinId).FirstOrDefault();

            if (pin != null)
            {
                var index = pin.Events.IndexOf(@event);

                if (index != -1)
                {
                    pin.Events.RemoveAt(index);
                    pin.Events.Insert(index, @event);
                }
                else
                {
                    pin.Events.Add(@event);
                }

                MessagingCenter.Send(App.Current, "pins_state_changed",
                        new PinsStateChangedMessage(new List<Pin>() { pin },
                        PinsStateChangedType.UpdateEvents));
            }



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
