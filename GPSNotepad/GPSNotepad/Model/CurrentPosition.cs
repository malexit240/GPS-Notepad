using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public static class CurrentPosition
    {
        public static async Task<Position> GetAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            return new Position(location.Latitude, location.Longitude);
        }
    }
}
