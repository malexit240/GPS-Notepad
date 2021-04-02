namespace GPSNotepad.Extensions
{
    public static class PinViewModelExtension
    {
        public static PinViewModel GetViewModel(this GPSNotepad.Model.Entities.Pin pin)
        {
            return new PinViewModel(pin.PinId, pin.UserId)
            {

                Name = pin.Name,
                Description = pin.Description,
                Position = pin.Position,
                Favorite = pin.Favorite
            };
        }

        public static GPSNotepad.Model.Entities.Pin GetModelPin(this PinViewModel pin)
        {
            return new Model.Entities.Pin()
            {
                PinId = pin.PinId,
                UserId = pin.UserId,
                Name = pin.Name,
                Description = pin.Description,
                Position = pin.Position,
                Favorite = pin.Favorite
            };
        }

        public static Xamarin.Forms.GoogleMaps.Pin GetGoogleMapsPin(this PinViewModel pin)
        {
            return new Xamarin.Forms.GoogleMaps.Pin()
            {
                Label = pin.Name,
                Position = pin.Position
            };
        }
    }
}
