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

namespace GPSNotepad.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Pin> _pins;
        protected IPinService PinService { get; set; }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public ICommand GoToAddPinForm { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPinService pinService) : base(navigationService)
        {
            PinService = pinService;
            pinService.GetAllPinsForUser(CurrentUser.Instance.UserId);
            GoToAddPinForm = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync(nameof(AddPinPage));
            });

            Pins = new ObservableCollection<Pin>();
            MessagingCenter.Subscribe<Prism.PrismApplicationBase>(App.Current, "pin_state_updated", OnPrismStateUpdated);
        }

        private void OnPrismStateUpdated(PrismApplicationBase obj)
        {
            Pins = new ObservableCollection<Pin>(PinService.GetAllPinsForUser(CurrentUser.Instance.UserId));
        }
    }
}
