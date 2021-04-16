using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;
using ZXing.QrCode;
using GPSNotepad.Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ZXing.Net.Mobile.Forms;

namespace GPSNotepad.Services.QRCodeService
{

    [Serializable]
    struct SerializablePin
    {
        public string Name;

        public string Description;

        public bool Favorite;

        public double Longitude;

        public double Latitude;

        public static SerializablePin? CreateFromBase64String(string source)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SerializablePin? pin = null;

            try
            {
                byte[] byteArray = Convert.FromBase64String(source);

                using var stream = new MemoryStream();

                for (int i = 0; i < byteArray.Length; i++)
                {
                    stream.WriteByte(byteArray[i]);
                }

                stream.Seek(0, SeekOrigin.Begin);

                pin = formatter.Deserialize(stream) as SerializablePin?;
            }
            finally { }

            return pin;

        }

        public Pin ToModelPin()
        {
            return new Pin()
            {
                Name = this.Name,
                Description = this.Description,
                Favorite = this.Favorite,
                Latitude = this.Latitude,
                Longitude = this.Longitude
            };
        }

        public string GetBase64Form()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            byte[] byteArray;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);

                stream.Seek(0, SeekOrigin.Begin);

                byteArray = new byte[stream.Length];

                for (int i = 0; i < stream.Length; i++)
                {
                    byteArray[i] = Convert.ToByte(stream.ReadByte());
                }
            }

            return Convert.ToBase64String(byteArray);
        }
    }

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


    public interface IQrScanerService
    {
        Task<Pin> GetPinAsync();
    }

    public class QrScanerService : IQrScanerService
    {
        public async Task<Pin> GetPinAsync()
        {
            return await Task.Run(async () =>
            {
                string source = await ScanAsync();

                return SerializablePin.CreateFromBase64String(source)?.ToModelPin();
            });
        }

        private async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}
