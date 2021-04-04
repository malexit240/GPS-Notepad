using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using GPSNotepad.Model;
using Xamarin.Forms;
using Prism;
using System.Windows.Input;
using GPSNotepad.Views;
using System.Linq;
using GPSNotepad.Extensions;
using System;

namespace GPSNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private UniqueObservableCollection<PinViewModel> _pins;
        protected IPinService PinService { get; set; }

        public UniqueObservableCollection<PinViewModel> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public ICommand GoToAddPinForm { get; set; }
        public DelegateCommand<PinViewModel> PinTappedCommand { get; set; }

        public MainMapViewModel MainMapViewModel { get; set; }

        private int _choosenPage = 0;
        public int ChoosenPage
        {
            get => _choosenPage;
            set => SetProperty(ref _choosenPage, value);
        }

        public MainPageViewModel(INavigationService navigationService, IPinService pinService) : base(navigationService)
        {
            PinService = pinService;

            MainMapViewModel = new MainMapViewModel(navigationService);

            Pins = new UniqueObservableCollection<PinViewModel>();

            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinStateChanged);

            pinService.GetAllPinsForUser(CurrentUser.Instance.UserId);

            PinTappedCommand = new DelegateCommand<PinViewModel>(item =>
            {

                ChoosenPage = 0;
                MainMapViewModel.Span = new Xamarin.Forms.GoogleMaps.MapSpan(item.Position, 0.01, 0.01);

            });


            GoToAddPinForm = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync(nameof(AddPinPage));
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        private void OnPinStateChanged(PrismApplicationBase obj, PinsStateChangedMessage message)
        {
            switch (message.ChangedType)
            {
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
