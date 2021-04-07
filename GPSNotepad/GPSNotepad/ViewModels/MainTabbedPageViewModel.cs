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
using System.Collections.Generic;

namespace GPSNotepad.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    {
        public MainTabbedPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService) : base(navigationService)
        {
            PinService = pinService;

            MainMapViewModel = new MainMapTabViewModel(navigationService);

            Pins = new UniqueObservableCollection<PinViewModel>();

            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);

            pinService.LoadUserPins(authorizationService.GetCurrenUserId());
        }

        protected IPinService PinService { get; set; }

        private UniqueObservableCollection<PinViewModel> _pins;
        public UniqueObservableCollection<PinViewModel> Pins
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
                SetProperty(ref _searchField, value);
                var comparer = PinService.Find(_searchField) ?? new PinPositionComparer(CurrentPosition.LastChecked);
                SortPins(comparer);
            }
        }

        private int _choosenPage = 0;
        public int ChoosenPage
        {
            get => _choosenPage;
            set => SetProperty(ref _choosenPage, value);
        }

        public MainMapTabViewModel MainMapViewModel { get; set; }


        #region ---Commands---

        private ICommand _editPinContextCommand;
        public ICommand EditPinContextCommand => _editPinContextCommand ??= new DelegateCommand<PinViewModel>(EditPinContextHandler);
        private async void EditPinContextHandler(PinViewModel pin)
        {
            await NavigationService.NavigateAsync(nameof(AddPinPage), (nameof(PinViewModel), pin));
        }


        private ICommand _goToAddPinFormCommand = null;
        public ICommand GoToAddPinFormCommand => _goToAddPinFormCommand ??= new DelegateCommand(GoToAddPinFormHandler);
        private void GoToAddPinFormHandler() => NavigationService.NavigateAsync(nameof(AddPinPage));


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
            Guid id;
            if (!Guid.TryParse(pin.Label, out id))
                return;
            SelectedPin = (from p in Pins where p.PinId == id select p).FirstOrDefault();
            MainMapViewModel.ShowDetailView = true;
        }
        #endregion

        private void SortPins(IComparer<Pin> comparer)
        {
            var pins = Pins.Select(p => p.GetModelPin()).ToList();
            pins.Sort(comparer);
            Pins = new UniqueObservableCollection<PinViewModel>(pins.Select(p => p.GetViewModel()).ToList());

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
                        break;
                    Pins[index] = pin;
                    break;
                case PinsStateChangedType.Delete:
                    Pins.Remove(message.ChangedPin.GetViewModel());
                    break;
            }
            CurrentPosition.GetAsync().
                ContinueWith(result =>
                {
                    SortPins(new PinPositionComparer(result.Result));
                });

        }
    }
}
