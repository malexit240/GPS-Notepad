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
using GPSNotepad.Model.Entities;
using GPSNotepad.Services.PinService;

namespace GPSNotepad
{
    public class PinViewModel : ViewModelBase, IEntityBase
    {
        private Guid _pinId;
        private Guid _userId;
        private string _name = "";
        private string _description = "";
        private Position _position = new Position();
        private bool _favorite = false;


        public bool HideInfoWindow { get; set; } = true;

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

            IPinService pinService = App.Current.Container.Resolve<IPinService>();

            EditPinContextCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync(nameof(AddPinPage), (nameof(PinViewModel), this));
            });

            DeletePinContextCommand = new DelegateCommand(() =>
            {
                pinService.Delete(this.GetModelPin());
            });

        }

        public Guid Id => _pinId;


        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            if (!(obj is IEntityBase))
                return false;

            var pin = (IEntityBase)obj;

            return this.PinId == pin.Id;
        }
    }
}
