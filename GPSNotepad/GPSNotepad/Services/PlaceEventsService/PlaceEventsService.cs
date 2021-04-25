using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPSNotepad.Entities;
using GPSNotepad.Repositories;
using System.Linq;
using GPSNotepad.Services.PinService;

namespace GPSNotepad.Services.PlaceEventsService
{
    public class PlaceEventsService : IPlaceEventsService
    {
        #region ---IPlaceEventsService Implementation---
        public async Task<List<PlaceEvent>> GetEventsForPin(Guid pinId)
        {
            return await Task.Run(() =>
            {
                using var context = new Context();

                return context.Events.Select(e => e).Where(e => e.PinId == pinId).ToList();
            });
        }

        public async void Create(PlaceEvent placeEvent)
        {
            PinState.Instance.UpdateEvent(placeEvent.PinId, placeEvent);

            await Task.Run(() =>
            {
                using var context = new Context();

                context.Events.Add(placeEvent);
                context.SaveChanges();
            });
        }

        public async void CreateOrUpdate(PlaceEvent placeEvent)
        {
            await Task.Run(() =>
            {
                using var context = new Context();

                if (context.Events.Any(e => e.PlaceEventId == placeEvent.PlaceEventId))
                {
                    this.Update(placeEvent);
                }
                else
                {
                    this.Create(placeEvent);
                }
            });

        }

        public async void Update(PlaceEvent placeEvent)
        {
            PinState.Instance.UpdateEvent(placeEvent.PinId, placeEvent);

            await Task.Run(() =>
            {
                using var context = new Context();

                context.Events.Update(placeEvent);
                context.SaveChanges();
            });
        }

        public async void Delete(PlaceEvent placeEvent)
        {
            PinState.Instance.DeleteEvent(placeEvent.PinId, placeEvent);

            await Task.Run(() =>
            {
                using var context = new Context();

                context.Events.Remove(placeEvent);
                context.SaveChanges();
            });
        }
        #endregion
    }
}
