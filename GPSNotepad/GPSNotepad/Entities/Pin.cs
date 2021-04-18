using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Entities
{
    public class Pin : IEntityBase
    {
        #region ---Model Fileds---
        public Guid PinId { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool Favorite { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public List<PlaceEvent> Events { get; set; } = new List<PlaceEvent>();

        #endregion

        #region ---Public Properties---
        [NotMapped]
        public Position Position
        {
            get => new Position(Latitude, Longitude);
            set
            {
                Longitude = value.Longitude;
                Latitude = value.Latitude;
            }
        }
        #endregion

        #region ---IEntityBase Implementation---
        [NotMapped]
        public Guid Id => PinId;
        #endregion


        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            if (!(obj is IEntityBase))
                return false;

            var pin = (IEntityBase)obj;

            return this.PinId == pin.Id;
        }
        #endregion

    }
}
