using GPSNotepad.Entities;

namespace GPSNotepad.Services.QRCodeService
{
    public class QrScanerService : IQrScanerService
    {
        #region ---IQrScanerService Implementation---

        public Pin GetPinFromString(string result)
        {
            result ??= string.Empty;

            var pin = SerializablePin.CreateFromBase64String(result)?.ToModelPin();

            return pin;
        }
        #endregion
    }
}
