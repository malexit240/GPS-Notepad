namespace GPSNotepad.Extensions
{
    public static class PlaceEventViewModelExtension
    {
        public static PlaceEventViewModel ToViewModel(this GPSNotepad.Model.Entities.PlaceEvent placeEvent)
        {
            return new PlaceEventViewModel(placeEvent.PlaceEventId, placeEvent.PinId, placeEvent.Time, placeEvent.Description);
        }

        public static GPSNotepad.Model.Entities.PlaceEvent ToModelEntity(this PlaceEventViewModel placeEvent)
        {
            return new GPSNotepad.Model.Entities.PlaceEvent()
            {
                PlaceEventId = placeEvent.PlaceEventId,
                PinId = placeEvent.PinId,
                Time = placeEvent.Time,
                Description = placeEvent.Description
            };
        }
    }
}
