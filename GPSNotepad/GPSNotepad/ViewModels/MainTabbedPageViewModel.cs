using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using GPSNotepad.Entities;
using GPSNotepad.Services.PinService;
using GPSNotepad.Model;
using Xamarin.Forms;
using Prism;
using System.Windows.Input;
using GPSNotepad.Views;
using System.Linq;
using GPSNotepad.Extensions;
using System;
using GPSNotepad.Services.Authorization;
using System.Collections.Generic;
using GPSNotepad.Services.QRCodeService;
using GPSNotepad.Comparers;
using GPSNotepad.Enums;
using System.ComponentModel;

namespace GPSNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region ---Constructors---
        public MainPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService, IQrScanerService qrScanerService) : base(navigationService)
        {
            PinService = pinService;
            AuthorizationService = authorizationService;
            QrScanerService = qrScanerService;

            MainMapViewModel = new MainMapViewModel(navigationService);

            Pins = new ObservableCollection<PinViewModel>();

            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);

            pinService.LoadUserPins(AuthorizationService.GetCurrenUserId());
        }
        #endregion

        #region ---Protected Properties---
        protected IPinService PinService { get; set; }
        protected IQrScanerService QrScanerService { get; set; }
        protected IAuthorizationService AuthorizationService { get; set; }

        #endregion

        #region ---Public Properties---
        private ObservableCollection<PinViewModel> _pins;
        public ObservableCollection<PinViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private PinViewModel _selectedPin = null;
        public PinViewModel SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }

        private string _searchField = "";
        public string SearchField
        {
            get => _searchField;
            set
            {
                if (_searchField == value)
                    return;
                SetProperty(ref _searchField, value);

                Pins = new ObservableCollection<PinViewModel>(PinService.Find(_searchField).Select(p => p.ToViewModel()));
            }
        }

        private int _choosenPage = 0;
        public int ChoosenPage
        {
            get => _choosenPage;
            set => SetProperty(ref _choosenPage, value);
        }

        public bool _isDropDownPinsVisible = false;
        public bool IsDropDownPinsVisible
        {
            get => _isDropDownPinsVisible;
            set => SetProperty(ref _isDropDownPinsVisible, value);
        }



        public bool _iSearchFieldFoccused = false;
        public bool IsSearchFieldFoccused
        {
            get => _iSearchFieldFoccused;
            set => SetProperty(ref _iSearchFieldFoccused, value);
        }

        public MainMapViewModel MainMapViewModel { get; set; }
        #endregion

        #region ---Commands---

        private ICommand _editPinContextCommand;
        public ICommand EditPinContextCommand => _editPinContextCommand ??= new DelegateCommand<PinViewModel>(EditPinContextHandler);
        private async void EditPinContextHandler(PinViewModel pin)
        {
            // await NavigationService.NavigateAsync(nameof(AddEditPinAndEventsTabbedPage), (nameof(PinViewModel), pin));
        }

        private ICommand _showQRCodeCommand;
        public ICommand ShowQRCodeCommand => _showQRCodeCommand ??= new DelegateCommand<PinViewModel>(ShowQRCodeHandler);
        private void ShowQRCodeHandler(PinViewModel pin)
        {
            // var parameters = new NavigationParameters();
            // parameters.Add(nameof(QRCodeModalViewModel.QRCodeValue), pin.GetModelPin().GetPinAsQRCode());
            // NavigationService.NavigateAsync(nameof(QRCodeModalPage), parameters, useModalNavigation: true, false);
        }

        private ICommand _scanQRCommand;
        public ICommand ScanQRCommand => _scanQRCommand ??= new DelegateCommand(ScanQRHandler);
        private async void ScanQRHandler()
        {
            var pin = await QrScanerService.GetPinAsync();

            if (pin != null)
            {
                pin.PinId = Guid.NewGuid();
                pin.UserId = AuthorizationService.GetCurrenUserId();
                await PinService.Create(pin);
            }
        }


        private ICommand _goToAddPinFormCommand = null;
        //  public ICommand GoToAddPinFormCommand => _goToAddPinFormCommand ??= new DelegateCommand(GoToAddPinFormHandler);
        // private void GoToAddPinFormHandler() => NavigationService.NavigateAsync(nameof(AddEditPinAndEventsTabbedPage));


        private ICommand _hideDetailViewCommand;
        public ICommand HideDetailPinViewCommand => _hideDetailViewCommand ??= new DelegateCommand(HideDetailPinViewHandler);
        private void HideDetailPinViewHandler()
        {
            SelectedPin = null;
            MainMapViewModel.ShowDetailView = false;
        }


        private DelegateCommand<PinViewModel> _pinTappedCommand;
        public DelegateCommand<PinViewModel> PinTappedCommand => _pinTappedCommand ??= new DelegateCommand<PinViewModel>(PinTappedHandler);
        private void PinTappedHandler(PinViewModel pin)
        {
            ChoosenPage = 0;
            MainMapViewModel.Span = new Xamarin.Forms.GoogleMaps.MapSpan(pin.Position,
                MainMapViewModel.Span.LatitudeDegrees,
                MainMapViewModel.Span.LongitudeDegrees);
        }


        private DelegateCommand<Xamarin.Forms.GoogleMaps.Pin> _onShowDetailPinViewCommand;
        public DelegateCommand<Xamarin.Forms.GoogleMaps.Pin> OnShowDetailPinViewCommand => _onShowDetailPinViewCommand ??= new DelegateCommand<Xamarin.Forms.GoogleMaps.Pin>(OnShowDetailPinViewHandler);
        private void OnShowDetailPinViewHandler(Xamarin.Forms.GoogleMaps.Pin pin)
        {
            if (!Guid.TryParse(pin.Label, out Guid id))
                return;
            SelectedPin = (from p in Pins where p.PinId == id select p).FirstOrDefault();
            MainMapViewModel.ShowDetailView = true;
        }

        private ICommand _logoutCommand;
        public ICommand LogoutCommand => _logoutCommand ??= new DelegateCommand(LogoutCommandHandler);
        private void LogoutCommandHandler()
        {
            AuthorizationService.LogOut();
            this.NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }


        #endregion

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(IsSearchFieldFoccused):

                    IsDropDownPinsVisible = IsSearchFieldFoccused;
                    break;
            }

            base.OnPropertyChanged(args);
        }

        #region ---Private Helpers---
        private void SortPins(IComparer<Pin> comparer)
        {
            var pins = Pins.Select(p => p.GetModelPin()).ToList();
            pins.Sort(comparer);
            Pins = new ObservableCollection<PinViewModel>(pins.Select(p => p.ToViewModel()).ToList());
        }

        private void OnPinStateChanged(PrismApplicationBase obj, PinsStateChangedMessage message)
        {
            switch (message.ChangedType)
            {
                case PinsStateChangedType.Load:
                case PinsStateChangedType.Add:
                    foreach (var p in message.NewPins)
                        Pins.Add(p.ToViewModel());
                    break;
                case PinsStateChangedType.Update:
                    var pin = message.ChangedPin.ToViewModel();
                    var index = Pins.IndexOf(pin);
                    if (index == -1)
                        break;
                    Pins[index] = pin;
                    break;
                case PinsStateChangedType.Delete:
                    Pins.Remove(message.ChangedPin.ToViewModel());
                    break;
            }
            CurrentPosition.GetAsync().
                ContinueWith(result =>
                {
                    SortPins(new PinPositionComparer(result.Result));
                });

        }
        #endregion
    }
}
