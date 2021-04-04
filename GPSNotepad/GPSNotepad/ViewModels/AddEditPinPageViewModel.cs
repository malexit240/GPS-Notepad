using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Model;
using System.Windows.Input;
using System;
using GPSNotepad.Model.Interfaces;
using GPSNotepad.Extensions;


namespace GPSNotepad.ViewModels
{
    public class AddEditPinPageViewModel : ViewModelBase
    {

        #region ---Public Properties---

        private MapSpan _span;
        private string _textCoordinates = "";

        public string TextCoordinates
        {
            get => _textCoordinates;
            set
            {
                if (_textCoordinates == value)
                    return;

                Position position;
                if (StringToPositionConverter.TryGetPosition(out position, value))
                {
                    SetNewPinPosition(position);
                    Span = new MapSpan(position, Span.LatitudeDegrees, Span.LongitudeDegrees);
                }
            }
        }

        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        public UniqueObservableCollection<PinViewModel> Pins { get; set; }

        public PinViewModel PinViewModel { get; set; }

        public ICommand AddEditPinCommand { get; set; }
        public DelegateCommand<object> OnMapClickedCommand { get; set; }
        public DelegateCommand OnMapLoadedCommand { get; set; }
        #endregion

        #region ---Constructors---
        public AddEditPinPageViewModel(INavigationService navigationService, IPinService pinService) : base(navigationService)
        {
            Pins = new UniqueObservableCollection<PinViewModel>();


            AddEditPinCommand = new DelegateCommand(() =>
            {
                pinService.CreateOrUpdatePin(PinViewModel.GetModelPin());
            });

            OnMapLoadedCommand = new DelegateCommand(() =>
            {
                if (PinViewModel == null)
                {
                    CurrentPosition.GetAsync().ContinueWith((position) =>
                    {
                        PinViewModel = new PinViewModel(NavigationService, Guid.NewGuid(), CurrentUser.Instance.UserId)
                        {
                            Position = position.Result

                        };
                        Pins.Add(PinViewModel);

                        Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);

                        _textCoordinates = StringToPositionConverter.ToFormatString(PinViewModel.Position);
                        RaisePropertyChanged(nameof(TextCoordinates));

                    });
                }
                else
                {
                    Pins.Add(PinViewModel);

                    Span = new MapSpan(PinViewModel.Position, 0.01, 0.01);

                    _textCoordinates = StringToPositionConverter.ToFormatString(PinViewModel.Position);
                    RaisePropertyChanged(nameof(TextCoordinates));
                }

            });


            OnMapClickedCommand = new DelegateCommand<object>((newPosition) =>
            {
                var position = (Position)newPosition;

                SetNewPinPosition(position);
            });

        }
        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(GPSNotepad.PinViewModel)))
                PinViewModel = (PinViewModel)parameters[nameof(GPSNotepad.PinViewModel)];
        }

        private void SetNewPinPosition(Position position)
        {
            if (PinViewModel.Position.Rounded() == position.Rounded())
                return;
            PinViewModel.Position = position.Rounded();
            Pins[0] = PinViewModel;
            _textCoordinates = StringToPositionConverter.ToFormatString(position.Rounded());
            RaisePropertyChanged(nameof(TextCoordinates));
        }
    }
}
