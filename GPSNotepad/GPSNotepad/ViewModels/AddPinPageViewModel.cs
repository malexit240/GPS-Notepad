﻿using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Model;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class AddPinPageViewModel : ViewModelBase
    {

        #region ---Public Properties---
        private string _name = "";
        private string _description = "";
        private string _textCoordinates = "";

        private Position _position = new Position(41.8, 12.46);
        private MapSpan _span;

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
        public string Coordinate
        {
            get => _textCoordinates;
            set
            {
                _textCoordinates = value;

                if (StringToPositionConverter.TryGetPosition(out _position, value))
                    if (Position != _position)
                        Position = _position;
            }
        }

        public Position Position
        {
            get => _position;
            set
            {
                if (_textCoordinates != StringToPositionConverter.ToFormatString(value))
                    Coordinate = StringToPositionConverter.ToFormatString(value);

                SetProperty(ref _position, value);

                RaisePropertyChanged("Coordinate");
                PinPosition = _position;
            }
        }

        public Position PinPosition
        {
            get => Position;
            set
            {
                Pins.Clear();
                Pins.Add(new Pin() { Label = Name, Position = value });
                RaisePropertyChanged(nameof(PinPosition));
            }
        }

        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        public UniqueObservableCollection<Pin> Pins { get; set; }

        public ICommand AddPinCommand { get; set; }
        public DelegateCommand<object> OnCameraMovingCommand { get; set; }
        #endregion

        #region ---Constructors---
        public AddPinPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Pins = new UniqueObservableCollection<Pin>();
            //Position = new Position(0, 0);
            OnCameraMovingCommand = new DelegateCommand<object>((position) =>
            {
                Position = (Position)position;
            });

        }
        #endregion
    }
}
