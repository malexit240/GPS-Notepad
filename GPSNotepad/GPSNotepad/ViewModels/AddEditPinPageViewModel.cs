using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Model;
using System.Windows.Input;
using System;
using GPSNotepad.Extensions;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.PinService;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinPageViewModel : ViewModelBase
    {

        #region ---Public Properties---

        private MapSpan _span;
        private string _textCoordinates = "";
        private string _name = "";
        private string _description;

        public UniqueObservableCollection<PinViewModel> Pins { get; set; }

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
        #endregion

        protected IAuthorizationService AuthorizationService { get; set; }

        #region ---Constructors---
        public AddEditPinPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IPinService pinService) : base(navigationService)
        {
            Pins = new UniqueObservableCollection<PinViewModel>();
            AuthorizationService = authorizationService;
            AddEditPinCommand = new DelegateCommand(() =>
            {
                PinViewModel.Name = Name;
                PinViewModel.Description = Description;
                pinService.CreateOrUpdatePin(PinViewModel.GetModelPin());
            });

            OnMapLoadedCommand = new DelegateCommand(OnMapLoadedHelper);

            OnMapClickedCommand = new DelegateCommand<object>((newPosition) =>
            {
                SetNewPinPosition((Position)newPosition);
            });

        }
        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(GPSNotepad.PinViewModel)))
                PinViewModel = (PinViewModel)parameters[nameof(GPSNotepad.PinViewModel)];
        }

        private async void OnMapLoadedHelper()
        {
            if (PinViewModel == null)
            {
                var position = await CurrentPosition.GetAsync();

                PinViewModel = new PinViewModel(NavigationService, Guid.NewGuid(), AuthorizationService.GetCurrenUserId())
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
