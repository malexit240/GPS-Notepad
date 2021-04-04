using GPSNotepad.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Extensions;
using GPSNotepad.ViewModels;
using Prism.Navigation;
using GPSNotepad.Views;

namespace GPSNotepad
{
    public class PinViewModel : ViewModelBase
    {
        private Guid _pinId;
        private Guid _userId;
        private string _name = "";
        private string _description = "";
        private Position _position = new Position();
        private bool _favorite = false;

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
            get => _position.Rounded();
            set => SetProperty(ref _position, value.Rounded());
        }
        public bool Favorite { get; set; }

        public ICommand EditPinContextCommand { get; set; }
        public ICommand DeletePinContextCommand { get; set; }

        public PinViewModel(INavigationService navigationService, Guid pinId, Guid userId) : base(navigationService)
        {
            _pinId = pinId;
            _userId = userId;

            EditPinContextCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync(nameof(AddPinPage), (nameof(PinViewModel), this));
            });

            DeletePinContextCommand = new DelegateCommand(() =>
            {
                App.Current.Container.Resolve<IPinService>().DeletePin(this.GetModelPin());
            });

        }

        public override int GetHashCode() => HashCode.Combine(_pinId);
    }
}
