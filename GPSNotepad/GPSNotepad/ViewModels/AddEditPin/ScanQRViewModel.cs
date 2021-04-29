using GPSNotepad.Entities;
using GPSNotepad.Services.QRCodeService;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;
using GPSNotepad.Extensions;
using Xamarin.Forms;

namespace GPSNotepad.ViewModels
{
    public class ScanQRViewModel : ViewModelBase
    {
        #region ---Constructors---
        public ScanQRViewModel(INavigationService navigationService, IQrScanerService qrScanerService) : base(navigationService)
        {
            QrScanerService = qrScanerService;
        }
        #endregion

        protected IQrScanerService QrScanerService { get; set; }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private DelegateCommand<ZXing.Result> _onScanQRResultCommand;
        public DelegateCommand<ZXing.Result> OnScanQRResultCommand => _onScanQRResultCommand ??= new DelegateCommand<ZXing.Result>(OnScanQRResultHandler);

        private void OnScanQRResultHandler(ZXing.Result result)
        {
            var pin = QrScanerService.GetPinFromString(result.Text);

            if (pin != null)
            {
                App.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
                {
                    var parameters = new NavigationParameters()
                    {
                    {nameof(Pin), pin}
                    };

                    await NavigationService.GoBackAsync(parameters, true, false);
                });
            }
            else
            {
                ErrorMessage = TextResources["ErrorScaning"];
            }
        }

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand => _goBackCommand ??= new DelegateCommand(async () =>
        {
            await NavigationService.GoBackAsync();
        });



    }
}
