using System;
using System.Text.RegularExpressions;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public static class StringToPositionConverter
    {
        #region ---Public Static Methods ---

        public static bool TryParseLatitude(string latitude, out double value)
        {
            value = 0;
            bool result = false;

            if (double.TryParse(latitude, out value))
            {
                if (value <= 180 && value >= -180)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool TryParseLongitude(string longitude, out double value)
        {
            value = 0;
            bool result = false;

            if (double.TryParse(longitude, out value))
            {
                if (value <= 90 && value >= -90)
                {
                    result = true;
                }
            }

            return result;
        }

        public static Position? GetPosition(string request)
        {
            var coordinates = Regex.Matches(request, @"([\-\+]?([\,]?(\d))+([Nn]|[Ss]|[Ww]|[Ee]))");

            if (coordinates.Count != 2 ||//if coordinates more than two
                Regex.Matches(request, @"(\d{1,3}){1}([Nn]|[Ss])").Count > 1 ||
                Regex.Matches(request, @"(\d{1,3}){1}([Ww]|[Ee])").Count > 1)// or not contains pair latitude-longitude
                return null;
            var numericValue = "";

            foreach (Match match in Regex.Matches(coordinates[0].Value, @"[\-\+]?([\,]?(\d))"))//concat numeric values
                numericValue += match.Value;
            if (!double.TryParse(numericValue, out double latitude))
                return null;

            numericValue = "";
            foreach (Match match in Regex.Matches(coordinates[1].Value, @"[\-\+]?([\,]?(\d))"))//concat numeric values
                numericValue += match.Value;

            if (!double.TryParse(numericValue, out double longitude))
                return null;

            latitude *= Regex.IsMatch(coordinates[0].Value, @"[Ss]") ? -1 : 1;
            longitude *= Regex.IsMatch(coordinates[1].Value, @"[Ww]") ? -1 : 1;

            return new Position(latitude, longitude).Round();
        }
        #endregion

        #region ---Extension Methods---
        public static string ToFormatedString(this Position position)
        {
            var formatedString = "";

            formatedString += Math.Abs(position.Latitude);
            formatedString += position.Latitude >= 0 ? "N" : "S";

            formatedString += " ";

            formatedString += Math.Abs(position.Longitude);
            formatedString += position.Longitude >= 0 ? "E" : "W";

            return formatedString;
        }
        #endregion
    }
}
