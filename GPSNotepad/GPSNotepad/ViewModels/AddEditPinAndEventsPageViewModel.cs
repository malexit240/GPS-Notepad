using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GPSNotepad.Extensions;
using GPSNotepad.Model;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.PinService;
using GPSNotepad.Views;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinAndEventsPageViewModel : ViewModelBase
    {
        #region ---Public Properties---

        protected IPinService PinService { get; set; }

        private MapSpan _span;
        private string _textCoordinates = "";
        private string _name = "";
        private string _description;

        public UniqueObservableCollection<PinViewModel> Pins { get; set; }
        public bool IsEdit { get; private set; } = false;
        public PinViewModel PinViewModel { get; set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ObservableCollection<PlaceEventViewModel> _events;
        public ObservableCollection<PlaceEventViewModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        public string TextCoordinates
        {
            get => _textCoordinates;
            set
            {
                if (_textCoordinates == value)
                    return;

                Position? position = StringToPositionConverter.GetPosition(value);
                if (position != null)
                {
                    SetNewPinPosition(position.Value);
                    Span = new MapSpan(position.Value, Span.LatitudeDegrees, Span.LongitudeDegrees);
                }
            }
        }

        public ICommand AddEditPinCommand { get; set; }
        public DelegateCommand<object> OnMapClickedCommand { get; set; }
        public ICommand OnMapLoadedCommand { get; set; }
        public ICommand GoToAddPlaceEventFormCommand { get; set; }
        public ICommand EditPlaceEventContextCommand { get; set; }
        #endregion

        protected IAuthorizationService AuthorizationService { get; set; }

        #region ---Constructors---
        public AddEditPinAndEventsPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService) : base(navigationService)
        {
            Pins = new UniqueObservableCollection<PinViewModel>();
            AuthorizationService = authorizationService;
            PinService = pinService;
            AddEditPinCommand = new DelegateCommand(async () =>
            {
                PinViewModel.Name = Name;
                PinViewModel.Description = Description;
                await navigationService.GoBackAsync();
                await pinService.CreateOrUpdatePin(PinViewModel.GetModelPin());
            });

            EditPlaceEventContextCommand = new DelegateCommand<PlaceEventViewModel>(async (placeEvent) =>
            {
                var parameters = new NavigationParameters();

                parameters.Add(nameof(PlaceEventViewModel), placeEvent);

                await NavigationService.NavigateAsync(nameof(AddEditPlaceEventPage), parameters);
            });

            OnMapLoadedCommand = new DelegateCommand(OnMapLoadedHelper);

            OnMapClickedCommand = new DelegateCommand<object>((newPosition) =>
            {
                SetNewPinPosition((Position)newPosition);
            });

            MessagingCenter.Subscribe<Prism.PrismApplicationBase, PinsStateChangedMessage>(App.Current, "pins_state_changed", OnPinsStateChanged);

            GoToAddPlaceEventFormCommand = new DelegateCommand(async () =>
            {
                var parameters = new NavigationParameters();

                parameters.Add(nameof(PlaceEventViewModel.PinId), this.PinViewModel.PinId);

                await NavigationService.NavigateAsync(nameof(AddEditPlaceEventPage), parameters);
            });

        }

        private void OnPinsStateChanged(PrismApplicationBase app, PinsStateChangedMessage message)
        {
            if (message.ChangedType == PinsStateChangedType.UpdateEvents)
            {
                PinViewModel.Events.Clear();

                message.ChangedPin.Events.ForEach(e => PinViewModel.Events.Add(e.ToViewModel()));
            }
        }
        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(PlaceEventViewModel)))
            {
                if (PinViewModel != null)
                {
                    var placeEvent = (PlaceEventViewModel)parameters[nameof(PlaceEventViewModel)];
                    var index = PinViewModel.Events.IndexOf(placeEvent);

                    if (index == -1)
                    {
                        PinViewModel.Events.Add(placeEvent);
                    }
                    else
                    {
                        PinViewModel.Events.RemoveAt(index);
                        PinViewModel.Events.Insert(index, placeEvent);
                    }

                    if (IsEdit == true)
                    {
                        PinService.Update(PinViewModel.GetModelPin());
                    }
                }
            }

            if (parameters.ContainsKey(nameof(GPSNotepad.PinViewModel)))
            {
                IsEdit = true;
                PinViewModel = (PinViewModel)parameters[nameof(GPSNotepad.PinViewModel)];
            }

            Events = PinViewModel.Events;
        }

        private async void OnMapLoadedHelper()
        {

            if (PinViewModel == null)
            {
                var position = await CurrentPosition.GetAsync();

                PinViewModel = new PinViewModel(Guid.NewGuid(), AuthorizationService.GetCurrenUserId())
                {
                    Position = position
                };

                Pins.Add(PinViewModel);
                Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);

                _textCoordinates = StringToPositionConverter.ToFormatedString(PinViewModel.Position);
                RaisePropertyChanged(nameof(TextCoordinates));
            }
            else
            {
                Name = PinViewModel.Name;
                Description = PinViewModel.Description;

                Pins.Add(PinViewModel);

                Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);

                _textCoordinates = StringToPositionConverter.ToFormatedString(PinViewModel.Position);
                RaisePropertyChanged(nameof(TextCoordinates));
            }

        }

        private void SetNewPinPosition(Position position)
        {
            if (PinViewModel.Position.Rounded() == position.Rounded())
                return;

            PinViewModel.Position = position.Rounded();
            Pins[0] = PinViewModel;
            _textCoordinates = StringToPositionConverter.ToFormatedString(position.Rounded());
            RaisePropertyChanged(nameof(TextCoordinates));
        }

    }
}
