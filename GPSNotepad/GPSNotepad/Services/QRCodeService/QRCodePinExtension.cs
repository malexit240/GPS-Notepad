using GPSNotepad.Entities;

namespace GPSNotepad.Services.QRCodeService
{
    public static class QRCodePinExtension
    {
        public static string GetPinAsQRCode(this Pin pin)
        {
            return new SerializablePin()
            {
                Name = pin.Name,
                Description = pin.Description,
                Favorite = pin.Favorite,
                Longitude = pin.Longitude,
                Latitude = pin.Latitude
            }.GetBase64Form();
        }
    }
}
