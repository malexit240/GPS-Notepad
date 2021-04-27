using System.Threading.Tasks;
using GPSNotepad.Entities;

namespace GPSNotepad.Services.QRCodeService
{
    public interface IQrScanerService
    {
        Pin GetPinFromString(string result);
    }
}
