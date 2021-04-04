using System;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad
{
    public static class GoogleMapsPinExtension
    {
        public static Position Rounded(this Position position)
        {
            return new Position(Math.Round(position.Latitude, 7), Math.Round(position.Longitude, 7));
        }
    }
}
