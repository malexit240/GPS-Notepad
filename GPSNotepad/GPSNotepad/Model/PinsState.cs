using System;
using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public class PinsState
    {
        static PinsState _instance;
        public static PinsState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PinsState();
                return _instance;
            }
        }

        public PinsState()
        {
            Pins = App.Current.Container.Resolve<IPermanentPinService>().GetAllPinsForUser(CurrentUser.Instance.UserId).Result;
        }

        public List<Pin> Pins { get; set; }

        public void Create(Pin pin)
        {
            Pins.Add(pin);
            App.Current.Container.Resolve<IPermanentPinService>().CreatePin(pin);
        }

        public void Update(Pin pin)
        {
            var index = Pins.IndexOf(pin);

            if (index == -1)
                return;

            Pins.RemoveAt(index);
            Pins.Insert(index, pin);
            App.Current.Container.Resolve<IPermanentPinService>().UpdatePin(pin);
        }

        public void Delete(Pin pin)
        {
            Pins.Remove(pin);
            App.Current.Container.Resolve<IPermanentPinService>().DeletePin(pin);
        }
    }
}
