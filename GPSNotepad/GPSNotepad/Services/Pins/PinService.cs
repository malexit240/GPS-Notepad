using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using System.Linq;
using GPSNotepad.Repositories;
using System.Collections;

namespace GPSNotepad.Services.PinService
{
    public class PinService : IPinService
    {
        protected static PinState PinState { get; set; } = new PinState();

        #region ---IPermanentPinService Implementation---
        public async Task<bool> Create(Pin pin)
        {

            PinState.Create(pin);

            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Add(pin);
                    context.SaveChangesAsync();
                }
                return true;
            });
        }

        public async Task<List<Pin>> GetAllPinsForUser(Guid user_id)
        {
            if (PinState.IsLoaded == true)
            {
                return PinState.Pins;
            }
            else
            {
                return await Task.Run(() =>
                {
                    using (var context = new Context())
                    {
                        var pins =
                        (from pin in context.Pins
                         where pin.UserId == user_id
                         select pin).ToList();

                        return pins;
                    }
                });
            }
        }

        public async Task<bool> Update(Pin pin)
        {
            PinState.Update(pin);

            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Update(pin);
                    context.SaveChangesAsync();
                }
                return true;
            });

        }

        public async Task<bool> Delete(Pin pin)
        {

            PinState.Delete(pin);

            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Remove(pin);
                    context.SaveChangesAsync();
                }
                return true;
            });
        }

        public async void LoadUserPins(Guid userId)
        {
            if (PinState.IsLoaded == false)
            {
                await Task.Run(async () =>
                {
                    using (var context = new Context())
                    {
                        PinState.LoadPins(await this.GetAllPinsForUser(userId));
                    }

                });
            }
        }

        public async Task<bool> CreateOrUpdatePin(Pin pin)
        {
            bool result = false;

            if (PinState.ContainsPin(pin.PinId))
            {
                result = await this.Update(pin);
            }
            else
            {
                result = await this.Create(pin);
            }

            return result;
        }

        public IComparer<Pin> Find(string searchField)
        {
            IComparer<Pin> result = null;

            var position = StringToPositionConverter.GetPosition(searchField);
            if (position != null)
            {
                result = new PinPositionComparer(position.Value);
            }
            else
            {
                result = new PinNameDescriptionComparer(searchField);
            }

            return result;
        }
        #endregion
    }
}
