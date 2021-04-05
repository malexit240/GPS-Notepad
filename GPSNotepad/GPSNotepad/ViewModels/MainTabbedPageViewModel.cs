using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using GPSNotepad.Model.Entities;
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

namespace GPSNotepad.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        private UniqueObservableCollection<PinViewModel> _pins;
        private PinViewModel _selectedPin = null;
        protected IPinService PinService { get; set; }

        public UniqueObservableCollection<PinViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public PinViewModel SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }

        public ICommand GoToAddPinForm { get; set; }
        public DelegateCommand<PinViewModel> PinTappedCommand { get; set; }

        public MainMapTabViewModel MainMapViewModel { get; set; }

        private int _choosenPage = 0;
        public int ChoosenPage
        {
            get => _choosenPage;
            set => SetProperty(ref _choosenPage, value);
        }

        public DelegateCommand<Xamarin.Forms.GoogleMaps.Pin> OnShowDetaiPinViewCommand { get; set; }

        public DelegateCommand HideDetailPinView { get; set; }

        private bool _showDetailView = false;

        public bool ShowDetailView
        {
            get => _showDetailView;
            set => SetProperty(ref _showDetailView, value);
        }

        public MainTabbedPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService) : base(navigationService)
        {
            PinService = pinService;

            MainMapViewModel = new MainMapTabViewModel(navigationService);

            Pins = new UniqueObservableCollection<PinViewModel>();

            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);

            pinService.LoadUserPins(authorizationService.GetCurrenUserId());

            PinTappedCommand = new DelegateCommand<PinViewModel>(item =>
            {
                ChoosenPage = 0;
                MainMapViewModel.Span = new Xamarin.Forms.GoogleMaps.MapSpan(item.Position,
                    MainMapViewModel.Span.LatitudeDegrees,
                    MainMapViewModel.Span.LongitudeDegrees);
            });

            OnShowDetaiPinViewCommand = new DelegateCommand<Xamarin.Forms.GoogleMaps.Pin>((pin) =>
            {
                Guid id;
                if (!Guid.TryParse(pin.Label, out id))
                    return;
                SelectedPin = (from p in Pins where p.PinId == id select p).FirstOrDefault();
                ShowDetailView = true;
            });
            HideDetailPinView = new DelegateCommand(() =>
            {
                SelectedPin = null;
                ShowDetailView = false;
            });

            GoToAddPinForm = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync(nameof(AddPinPage));
            });
        }

        private void OnPinStateChanged(PrismApplicationBase obj, PinsStateChangedMessage message)
        {
            switch (message.ChangedType)
            {
                case PinsStateChangedType.Load:
                case PinsStateChangedType.Add:
                    foreach (var p in message.NewPins)
                        Pins.Add(p.GetViewModel());
                    break;
                case PinsStateChangedType.Update:
                    var pin = message.ChangedPin.GetViewModel();
                    var index = Pins.IndexOf(pin);
                    if (index == -1)
                        return;
                    Pins[index] = pin;
                    break;
                case PinsStateChangedType.Delete:
                    Pins.Remove(message.ChangedPin.GetViewModel());
                    break;
            }
        }
    }
}
