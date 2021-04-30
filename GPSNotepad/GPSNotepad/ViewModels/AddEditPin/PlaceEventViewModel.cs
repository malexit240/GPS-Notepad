using Prism.Mvvm;
using System;
using GPSNotepad.Entities;

namespace GPSNotepad.ViewModels
{
    public class PlaceEventViewModel : BindableBase, IEntityBase
    {
        #region ---Constructors---
        public PlaceEventViewModel(Guid placeEventId, Guid pinId, DateTime time, string description)
        {
            PlaceEventId = placeEventId;
            PinId = pinId;
            Time = time;
            Description = description;
        }

        #endregion

        #region ---Public Properties---

        public PlaceEventViewModel Self => this;

        private Guid _placeEventId;
        public Guid PlaceEventId
        {
            get => _placeEventId;
            set => SetProperty(ref _placeEventId, value);
        }

        private Guid _pinId;
        public Guid PinId
        {
            get => _pinId;
            set => SetProperty(ref _pinId, value);
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string TimeString => Time.ToShortTimeString();
        public string DateString => Time.ToShortDateString();

        #endregion

        #region ---IEntityBase Implementation---

        public Guid Id => _placeEventId;

        #endregion

        #region ---Overrides---

        public override int GetHashCode() => HashCode.Combine(Id);

        public override bool Equals(object obj)
        {
            if (!(obj is IEntityBase))
                return false;

            var pin = (IEntityBase)obj;

            return this.PlaceEventId == pin.Id;
        }

        #endregion
    }
}
