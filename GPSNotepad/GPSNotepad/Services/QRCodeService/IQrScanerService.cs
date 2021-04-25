using System.Threading.Tasks;
using GPSNotepad.Entities;

namespace GPSNotepad.Services.QRCodeService
{
    public interface IQrScanerService
    {
        Task<Pin> GetPinAsync();
    }
}
