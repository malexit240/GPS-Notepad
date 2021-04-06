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

        protected IPinService PinService { get; set; }


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


        public bool Favorite
        {
            get => _favorite;

            set
            {
                SetProperty(ref _favorite, value);
                PinService?.Update(this.GetModelPin());
            }
        }

        public ICommand EditPinContextCommand { get; set; }
        public ICommand DeletePinContextCommand { get; set; }

        public PinViewModel(INavigationService navigationService, Guid pinId, Guid userId, string name = "", string description = "", bool favorite = false, Position? position = null) : base(navigationService)
        {
            _pinId = pinId;
            _userId = userId;

            _name = name;
            _description = description;
            _favorite = favorite;
            _position = position ?? new Xamarin.Forms.GoogleMaps.Position();

            PinService = App.Current.Container.Resolve<IPinService>();

            EditPinContextCommand = new DelegateCommand(() =>
            {
                navigationService.NavigateAsync(nameof(AddPinPage), (nameof(PinViewModel), this));
            });

            DeletePinContextCommand = new DelegateCommand(() =>
            {
                PinService.Delete(this.GetModelPin());
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
