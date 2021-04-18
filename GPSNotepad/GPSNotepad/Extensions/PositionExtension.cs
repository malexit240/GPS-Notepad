using Xamarin.Essentials;

namespace GPSNotepad.Extensions
{
    public static class PositionExtension
    {
        #region ---Extension Methods---
        public static double CalculateDistance(this Xamarin.Forms.GoogleMaps.Position thisPosition, Xamarin.Forms.GoogleMaps.Position position)
        {
            var location = new Location(thisPosition.Latitude, thisPosition.Longitude);

            return location.CalculateDistance(new Location(position.Latitude, position.Longitude), DistanceUnits.Kilometers);
        }
        #endregion
    }
}
