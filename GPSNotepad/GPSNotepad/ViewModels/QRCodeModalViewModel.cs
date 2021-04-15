using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.ViewModels
{
    public class QRCodeModalViewModel : ViewModelBase
    {
        private string _qRCodeValue;
        public string QRCodeValue
        {
            get => _qRCodeValue;
            set => SetProperty(ref _qRCodeValue, value);
        }

        public QRCodeModalViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(QRCodeModalViewModel.QRCodeValue)))
            {
                QRCodeValue = (string)parameters[nameof(QRCodeModalViewModel.QRCodeValue)];
            }
        }
    }
}
