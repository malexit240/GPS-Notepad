using System;
using System.Text.RegularExpressions;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Model
{
    public static class StringToPositionConverter
    {
        #region ---Public Static Methods ---
        public static bool TryGetPosition(out Position position, string request)
        {
            position = new Position();

            var coordinates = Regex.Matches(request, @"([\-\+]?([\,]?(\d))+([Nn]|[Ss]|[Ww]|[Ee]))");

            if (coordinates.Count != 2 ||//if coordinates more than two
                Regex.Matches(request, @"(\d{1,3}){1}([Nn]|[Ss])").Count > 1 ||
                Regex.Matches(request, @"(\d{1,3}){1}([Ww]|[Ee])").Count > 1)// or not contains pair latitude-longitude
                return false;

            double latitude = 0, longitude = 0;

            var numericValue = "";

            foreach (Match match in Regex.Matches(coordinates[0].Value, @"[\-\+]?([\,]?(\d))"))//concat numeric values
                numericValue += match.Value;

            if (!double.TryParse(numericValue, out latitude))
                return false;

            numericValue = "";
            foreach (Match match in Regex.Matches(coordinates[1].Value, @"[\-\+]?([\,]?(\d))"))//concat numeric values
                numericValue += match.Value;

            if (!double.TryParse(numericValue, out longitude))
                return false;

            latitude *= Regex.IsMatch(coordinates[0].Value, @"[Ss]") ? -1 : 1;
            longitude *= Regex.IsMatch(coordinates[1].Value, @"[Ww]") ? -1 : 1;

            position = new Position(latitude, longitude);

            return true;
        }
        #endregion

        #region ---Extension Methods---
        public static string ToFormatString(this Position position)
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
