using System.Threading.Tasks;
using Xamarin.Essentials;
using ZXing.Mobile;
using GPSNotepad.Entities;
using GPSNotepad.Services.PermissionManager;

namespace GPSNotepad.Services.QRCodeService
{
    public class QrScanerService : IQrScanerService
    {
        #region ---Constructors---
        public QrScanerService()
        {
            PermissionManager = App.Current.Container.Resolve<IPermissionManager>();
        }
        #endregion

        #region ---Protected Properties---
        protected IPermissionManager PermissionManager { get; set; }
        #endregion

        #region ---IQrScanerService Implementation---
        public async Task<Pin> GetPinAsync()
        {
            return await Task.Run(async () =>
            {
                var source = await ScanAsync() ?? string.Empty;

                var pin = SerializablePin.CreateFromBase64String(source)?.ToModelPin();

                return pin;
            });
        }
        #endregion

        #region ---Private Helpers---
        private async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner();

            ZXing.Result scanResult = null;

            await PermissionManager.RunWithPermission<Permissions.Camera>(async () =>
            {
                scanResult = await scanner.Scan(optionsCustom);
            });

            return scanResult?.Text;
        }
        #endregion
    }
}
