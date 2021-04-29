using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;
using GPSNotepad.Extensions;
using GPSNotepad.Entities;
using GPSNotepad.Services.PinService;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using GPSNotepad.Converters;
using Acr.UserDialogs;
using Xamarin.Essentials;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;

namespace GPSNotepad.ViewModels
{
    public class PinViewModel : BindableBase, IEntityBase
    {
        #region ---Constructors---
        public PinViewModel(Guid pinId, Guid userId, string name = "", string description = "", bool favorite = false, Position? position = null, List<PlaceEvent> events = null)
        {
            _pinId = pinId;
            _userId = userId;
            _name = name;
            _description = description;
            _favorite = favorite;
            Position = position ?? new Xamarin.Forms.GoogleMaps.Position();

            Events = new ObservableCollection<PlaceEventViewModel>();

            events?.ForEach(e => Events.Add(e.ToViewModel()));

            PinService = App.Current.Container.Resolve<IPinService>();
        }
        #endregion

        #region ---Protected Properties---
        protected IPinService PinService { get; set; }
        #endregion

        #region ---Public Properties---
        public PinViewModel Self => this;


        private string _adress = "";
        public string Address
        {
            get => _adress;
            set => SetProperty(ref _adress, value);
        }

        public bool HideInfoWindow { get; set; } = true;

        private Guid _pinId;
        public Guid PinId
        {
            get => _pinId;
        }

        private Guid _userId;
        public Guid UserId
        {
            get => _userId;
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description = "";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private Position _position = new Position();
        public Position Position
        {
            get => _position.Round();
            set => SetProperty(ref _position, value.Round());
        }

        public string FormatedPosition
        {
            get => Position.ToFormatedString();
        }

        private bool _favorite = false;
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

        private ObservableCollection<PlaceEventViewModel> _events;
        public ObservableCollection<PlaceEventViewModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }
        #endregion

        #region ---Commands---
        private ICommand _deletePinContextCommand;
        public ICommand DeletePinContextCommand => _deletePinContextCommand ??= new DelegateCommand(() =>
        {
            UserDialogs.Instance.Confirm(new ConfirmConfig()
            {
                Title = GPSNotepad.Resources.UIResources.ConfirmTitle,

                Message = GPSNotepad.Resources.UIResources.ConfirmDeletePin,
                OkText = GPSNotepad.Resources.UIResources.Ok,
                CancelText = GPSNotepad.Resources.UIResources.Cancel,
                OnAction = (confirm) =>
                {
                    if (confirm)
                        PinService.Delete(this.GetModelPin());
                }
            });
        });
        #endregion

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Position):
                    Address = await GetAddress() ?? FormatedPosition;
                    break;
            }
        }

        private async Task<string> GetAddress()
        {
            var geocoder = new Geocoder();
            List<string> addresses = null;

            try
            {
                addresses = new List<string>(await geocoder.GetAddressesForPositionAsync(Position));
            }
            catch (Exception)
            {

            }

            return addresses?.FirstOrDefault();
        }

        #region ---IEntityBase Implementation---
        public Guid Id => _pinId;
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            var pin = (IEntityBase)obj;

            return this.PinId == pin?.Id;
        }
        #endregion
    }
}
