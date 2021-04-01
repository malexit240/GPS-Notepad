using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class AddPinPageViewModel : ViewModelBase
    {

        private string _name = "";
        private string _description = "";
        private string _textCoordinates = "";
        private Position _position = new Position(41.8, 12.46);

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
                if (StringPositionConverter.TryGetPosition(out _position, value))
                    Position = _position;
            }
        }

        public Position Position
        {
            get => _position;
            set
            {
                Pins.Clear();
                Pins.Add(new Pin() { Position = _position });

                SetProperty(ref _position, value);
            }
        }

        public ObservableCollection<Pin> Pins { get; set; }

        public ICommand AddPinCommand { get; set; }

        private MapSpan _span;
        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        public AddPinPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
