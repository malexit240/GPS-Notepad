using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Extensions;
using GPSNotepad.ViewModels;
using Prism.Navigation;
using GPSNotepad.Views;
using GPSNotepad.Entities;
using GPSNotepad.Services.PinService;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GPSNotepad
{
    public class PinViewModel : BindableBase, IEntityBase
    {
        private Guid _pinId;
        private Guid _userId;
        private string _name = "";
        private string _description = "";
        private Position _position = new Position();
        private bool _favorite = false;

        protected IPinService PinService { get; set; }


        public PinViewModel Self => this;

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
                if (_favorite == value)
                    return;
                SetProperty(ref _favorite, value);
                PinService?.Update(this.GetModelPin());
            }
        }

        public ICommand EditPinContextCommand { get; set; }
        public ICommand DeletePinContextCommand { get; set; }

        public PinViewModel(Guid pinId, Guid userId, string name = "", string description = "", bool favorite = false, Position? position = null, List<PlaceEvent> events = null)
        {
            _pinId = pinId;
            _userId = userId;

            _name = name;
            _description = description;
            _favorite = favorite;
            _position = position ?? new Xamarin.Forms.GoogleMaps.Position();

            Events = new ObservableCollection<PlaceEventViewModel>();

            events?.ForEach(e => Events.Add(e.ToViewModel()));

            PinService = App.Current.Container.Resolve<IPinService>();

            DeletePinContextCommand = new DelegateCommand(() =>
            {
                PinService.Delete(this.GetModelPin());
            });

        }

        private ObservableCollection<PlaceEventViewModel> _events;
        public ObservableCollection<PlaceEventViewModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
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
