using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public class PinStateService : IPinService
    {

        public List<Pin> GetAllPinsForUser(Guid user_id)
        {
            return PinsState.Instance.Pins;
        }

        public async Task<Pin> FindPin(string name)
        {
            return await Task.Factory.StartNew(() =>
            (from pin in PinsState.Instance.Pins where pin.Name == name select pin).First()
            );
        }

        public Pin CreatePin(Guid user_id, string name)
        {
            var pin = new Pin()
            {
                PinId = Guid.NewGuid(),
                Name = name,
                UserId = user_id,
                Favorite = false
            };

            PinsState.Instance.Create(pin);

            return pin;
        }


        public Pin UpdatePin(Pin pin)
        {
            PinsState.Instance.Update(pin);
            return pin;
        }

        public void DeletePin(Pin pin)
        {
            PinsState.Instance.Delete(pin);
        }
    }
}
