using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public class BufferedPinService : IPinService
    {

        public List<Pin> GetAllPinsForUser(Guid user_id)
        {
            return BufferedPins.Instance.Pins;
        }

        public async Task<Pin> FindPin(string name)
        {
            return await Task.Factory.StartNew(() =>
            (from pin in BufferedPins.Instance.Pins where pin.Name == name select pin).First()
            );
        }

        public Pin CreatePin(Guid user_id, string name)
        {
            var pin = new Pin()
            {
                PinId = Guid.NewGuid(),
                Name = name,
                UserId = user_id
            };

            BufferedPins.Instance.Create(pin);

            return pin;
        }


        public Pin UpdatePin(Pin pin)
        {
            BufferedPins.Instance.Update(pin);
            return pin;
        }

        public void DeletePin(Pin pin)
        {
            BufferedPins.Instance.Delete(pin);
        }
    }
}
