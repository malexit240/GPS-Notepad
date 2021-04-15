using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GPSNotepad.PlatformDependencyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(GPSNotepad.Droid.DroidQrScanerService))]
namespace GPSNotepad.Droid
{
    public class DroidQrScanerService : IQrScanerService
    {
        public async Task<string> ScanAsync()
        {
            return "";
            //var optionsDefault = new MobileBarcodeScanningOptions();
            //var optionsCustom = new MobileBarcodeScanningOptions();
            //
            //var scanner = new MobileBarcodeScanner()
            //{
            //    TopText = "Scan the QR Code",
            //    BottomText = "Please Wait",
            //};
            //
            //var scanResult = await scanner.Scan(optionsCustom);
            //return scanResult.Text;
        }
    }
}