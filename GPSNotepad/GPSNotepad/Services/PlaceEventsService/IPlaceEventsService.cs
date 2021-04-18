using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GPSNotepad.Entities;

namespace GPSNotepad.Services.PlaceEventsService
{
    public interface IPlaceEventsService
    {
        Task<List<PlaceEvent>> GetEventsForPin(Guid pinId);

        void Create(PlaceEvent placeEvent);

        void Update(PlaceEvent placeEvent);

        void Delete(PlaceEvent placeEvent);

        void CreateOrUpdate(PlaceEvent e);
    }
}
