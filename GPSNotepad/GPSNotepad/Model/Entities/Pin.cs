﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Model.Entities
{

    public class Pin : IUniqueElement
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
        [NotMapped]
        public Guid Id => PinId;
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            if (!(obj is IUniqueElement))
                return false;

            var pin = (IUniqueElement)obj;

            return this.PinId == pin.Id;
        }
        #endregion

    }
}
