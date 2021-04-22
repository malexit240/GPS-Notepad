using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GPSNotepad.Converters;
using GPSNotepad.Enums;
using GPSNotepad.Extensions;
using GPSNotepad.Model;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.PinService;
using GPSNotepad.Views;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinAndEventsViewModel : ViewModelBase
    {
        #region ---Constructors---
        public AddEditPinAndEventsViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService) : base(navigationService)
        {
            Pins = new ObservableCollection<PinViewModel>();
            AuthorizationService = authorizationService;
            PinService = pinService;
        }
        #endregion


        #region ---Protected Properties---
        protected IPinService PinService { get; set; }

        protected IAuthorizationService AuthorizationService { get; set; }
        #endregion

        #region ---Public Properties---

        public ObservableCollection<PinViewModel> Pins { get; set; }

        public bool IsEdit { get; private set; } = false;

        public PinViewModel PinViewModel { get; set; }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
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

        private MapSpan _span;
        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private bool _isLongitudeWrong;
        public bool IsLongitudeWrong
        {
            get => _isLongitudeWrong;
            set => SetProperty(ref _isLongitudeWrong, value);
        }

        private bool _isLatitudeWrong;
        public bool IsLatitudeWrong
        {
            get => _isLatitudeWrong;
            set => SetProperty(ref _isLatitudeWrong, value);
        }

        #endregion

        #region ---Commands---
        public ICommand AddEditPinCommand => new DelegateCommand(async () =>
        {
            PinViewModel.Name = Name;
            PinViewModel.Description = Description;
            await NavigationService.GoBackAsync();
            await PinService.CreateOrUpdatePin(PinViewModel.GetModelPin());
        });

        public ICommand EditPlaceEventContextCommand =>

        new DelegateCommand<PlaceEventViewModel>(async (placeEvent) =>
        {
            var parameters = new NavigationParameters
                {
                    { nameof(PlaceEventViewModel), placeEvent }
                };

            //   await NavigationService.NavigateAsync(nameof(AddEditPlaceEventPage), parameters);
        });

        public DelegateCommand<object> OnMapClickedCommand => new DelegateCommand<object>((newPosition) =>
        {
            SetNewPinPosition((Position)newPosition);
        });

        public ICommand OnMapLoadedCommand => new DelegateCommand(OnMapLoadedHelper);

        public ICommand GoToAddPlaceEventFormCommand => new DelegateCommand(async () =>
        {
            var parameters = new NavigationParameters();

            parameters.Add(nameof(PlaceEventViewModel.PinId), this.PinViewModel.PinId);

            // await NavigationService.NavigateAsync(nameof(AddEditPlaceEventPage), parameters);
        });

        #endregion

        #region ---Overrides---
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

            if (parameters.ContainsKey(nameof(GPSNotepad.ViewModels.PinViewModel)))
            {
                IsEdit = true;
                PinViewModel = (PinViewModel)parameters[nameof(GPSNotepad.ViewModels.PinViewModel)];
            }

            Events = PinViewModel.Events;
        }


        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Latitude):
                    if (!StringToPositionConverter.TryParseLatitude(Latitude, out double latitude))
                    {
                        IsLatitudeWrong = true;
                    }
                    else
                    {
                        IsLatitudeWrong = false;
                        if (latitude != PinViewModel.Position.Latitude)
                        {
                            SetNewPinPosition(new Position(latitude, PinViewModel.Position.Longitude));
                            Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);
                        }
                    }
                    break;

                case nameof(Longitude):
                    if (!StringToPositionConverter.TryParseLongitude(Longitude, out double longitude))
                    {
                        IsLongitudeWrong = true;
                    }
                    else
                    {
                        IsLongitudeWrong = false;
                        if (longitude != PinViewModel.Position.Longitude)
                        {
                            SetNewPinPosition(new Position(PinViewModel.Position.Latitude, longitude));
                            Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);
                        }
                    }
                    break;
            }

        }

        #endregion

        #region ---Private Helpers---

        private void OnPinsStateChanged(PrismApplicationBase _, PinsStateChangedMessage message)
        {
            if (message.ChangedType == PinsStateChangedType.UpdateEvents)
            {
                PinViewModel.Events.Clear();

                message.ChangedPin.Events.ForEach(e => PinViewModel.Events.Add(e.ToViewModel()));
            }
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
            }
            else
            {
                Name = PinViewModel.Name;
                Description = PinViewModel.Description;

                Pins.Add(PinViewModel);

                Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);
            }

        }

        private void SetNewPinPosition(Position position)
        {
            if (PinViewModel.Position.Rounded() == position.Rounded())
                return;

            PinViewModel.Position = position.Rounded();
            Pins[0] = PinViewModel;

            Latitude = PinViewModel.Position.Latitude.ToString();
            Longitude = PinViewModel.Position.Longitude.ToString();
        }
        #endregion
    }
}
