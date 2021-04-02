using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Model.Entities
{
    public class Pin
    {
        #region ---Model Fileds---
        public Guid PinId { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Favorite { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }


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

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(PinId);
        #endregion

    }
}
