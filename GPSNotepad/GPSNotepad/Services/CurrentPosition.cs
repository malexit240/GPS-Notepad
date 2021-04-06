using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public static class CurrentPosition
    {
        public static Position LastChecked { get; private set; } = new Position();

        #region ---Public Static Methods---
        public static async Task<Position> GetAsync()
        {
            Position position = LastChecked;

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                position = new Position(location.Latitude, location.Longitude);
            }
            catch (Exception)
            {

            }

            LastChecked = position;

            return position;
        }
        #endregion
    }
}
