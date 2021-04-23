using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.ViewModels
{
    public class QRCodeModalViewModel : ViewModelBase
    {
        #region ---Constructors---
        public QRCodeModalViewModel(INavigationService navigationService) : base(navigationService) { }
        #endregion

        #region ---Public Properties---
        private string _qRCodeValue;
        public string QRCodeValue
        {
            get => _qRCodeValue;
            set => SetProperty(ref _qRCodeValue, value);
        }
        #endregion

        #region ---Overrides---
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(QRCodeModalViewModel.QRCodeValue)))
            {
                QRCodeValue = (string)parameters[nameof(QRCodeModalViewModel.QRCodeValue)];
            }
        }
        #endregion
    }
}
