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
        #region ---Public Static Methods---
        public static async Task<Position> GetAsync()
        {
            Position position = new Position();

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                position = new Position(location.Latitude, location.Longitude);
            }
            catch (Exception)
            {

            }

            return position;
        }
        #endregion
    }
}
