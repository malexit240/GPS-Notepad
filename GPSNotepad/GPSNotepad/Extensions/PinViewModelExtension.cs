using Prism.Navigation;
using System.Linq;

namespace GPSNotepad.Extensions
{
    public static class PinViewModelExtension
    {
        public static PinViewModel ToViewModel(this GPSNotepad.Entities.Pin pin)
        {
            return new PinViewModel(pin.PinId, pin.UserId, pin.Name, pin.Description, pin.Favorite, pin.Position,pin.Events);
        }

        public static GPSNotepad.Entities.Pin GetModelPin(this PinViewModel pin)
        {
            return new Entities.Pin()
            {
                PinId = pin.PinId,
                UserId = pin.UserId,
                Name = pin.Name,
                Description = pin.Description,
                Position = pin.Position,
                Favorite = pin.Favorite,
                Events = pin.Events.Select(e => e.ToModelEntity()).ToList()
            };
        }

        public static Xamarin.Forms.GoogleMaps.Pin GetGoogleMapsPin(this PinViewModel pin)
        {
            return new Xamarin.Forms.GoogleMaps.Pin()
            {
                Label = pin.Name,
                Type = Xamarin.Forms.GoogleMaps.PinType.SavedPin,
                Position = pin.Position
            };
        }
    }
}
