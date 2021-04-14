using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using System.Linq;
using GPSNotepad.Repositories;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using GPSNotepad.Services.PlaceEventsService;

namespace GPSNotepad.Services.PinService
{
    public class PinService : IPinService
    {
        protected static PinState PinState { get; set; } = PinState.Instance;
        protected IPlaceEventsService PlaceEventService { get; set; }

        public PinService(IPlaceEventsService placeEventService)
        {
            PlaceEventService = placeEventService;
        }

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

                        return context.Pins.Include(p => p.Events).Select(p => p).Where(p => p.UserId == user_id).ToList();
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
                    pin.Events.ForEach(e => PlaceEventService.CreateOrUpdate(e));
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

        public IList<Pin> Find(string searchField)
        {

            var result = PinState.Pins.ToList();

            if (searchField != "")
            {
                var comparer = this.GetComparer(searchField);

                result.Sort(comparer);

                result = comparer.GetExclusion(result) as List<Pin>;
            }

            return result;
        }

        private ExcludedComparer<Pin> GetComparer(string searchField)
        {
            ExcludedComparer<Pin> result = null;

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
