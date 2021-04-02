using Prism.Mvvm;
using System;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad
{
    public class PinViewModel : BindableBase
    {
        private Guid _pinId;
        private Guid _userId;
        private string _name;
        private string _description;
        private Position _position;
        private bool _favorite;

        public Guid PinId
        {
            get => _pinId;
        }

        public Guid UserId
        {
            get => _userId;
        }

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
        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        public bool Favorite { get; set; }

        public PinViewModel(Guid pinId, Guid userId)
        {
            _pinId = pinId;
            _userId = userId;
        }

        public override int GetHashCode() => HashCode.Combine(_pinId);
    }
}
