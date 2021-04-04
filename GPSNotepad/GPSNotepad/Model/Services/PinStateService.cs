using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public class PinStateService : IPinService
    {

        #region ---IPinService Implementation---
        public List<Pin> GetAllPinsForUser(Guid user_id) => PinsState.Instance.Pins;

        public async Task<List<Pin>> FindPin(string request) // not implemented code
        {
            return await Task.Run(() =>
            {
                return new List<Pin>();
            });
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

        public void CreateOrUpdatePin(Pin pin)
        {
            if (!PinsState.Instance.ContainsPin(pin.PinId))
                PinsState.Instance.Create(pin);
            else
                PinsState.Instance.Update(pin);
        }

        public Pin UpdatePin(Pin pin)
        {
            PinsState.Instance.Update(pin);
            return pin;
        }

        public void DeletePin(Pin pin) => PinsState.Instance.Delete(pin);


        #endregion

    }
}
