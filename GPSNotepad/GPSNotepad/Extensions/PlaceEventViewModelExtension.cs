using GPSNotepad.ViewModels;

namespace GPSNotepad.Extensions
{
    public static class PlaceEventViewModelExtension
    {
        #region ---Extension Methods---
        public static PlaceEventViewModel ToViewModel(this GPSNotepad.Entities.PlaceEvent placeEvent)
        {
            return new PlaceEventViewModel(placeEvent.PlaceEventId, placeEvent.PinId, placeEvent.Time, placeEvent.Description);
        }

        public static GPSNotepad.Entities.PlaceEvent ToModelEntity(this PlaceEventViewModel placeEvent)
        {
            return new GPSNotepad.Entities.PlaceEvent()
            {
                PlaceEventId = placeEvent.PlaceEventId,
                PinId = placeEvent.PinId,
                Time = placeEvent.Time,
                Description = placeEvent.Description
            };
        }
        #endregion
    }
}
