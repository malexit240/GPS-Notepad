using GPSNotepad.Entities;
using GPSNotepad.Services.QRCodeService;

namespace GPSNotepad.Extensions
{
    public static class QRCodePinExtension
    {
        #region ---Extension Methods---
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
        #endregion
    }
}
