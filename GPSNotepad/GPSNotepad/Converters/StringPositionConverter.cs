using System;
using System.Text.RegularExpressions;
using Xamarin.Forms.GoogleMaps;


namespace GPSNotepad.Model
{
    public static class StringPositionConverter
    {
        public static bool TryGetPosition(out Position position, string request)
        {
            position = new Position();
            var coordinates = Regex.Matches(request, @"([\-\+]?([\,]?(\d))+([Nn]|[Ss]|[Ww]|[Ee]))");
            if (coordinates.Count != 2 ||
                Regex.Matches(request, @"(\d{1,3}){1}([Nn]|[Ss])").Count > 1 ||
                Regex.Matches(request, @"(\d{1,3}){1}([Ww]|[Ee])").Count > 1)
                return false;

            double latitude = 0, longitude = 0;


            var str = "";
            foreach (Match match in Regex.Matches(coordinates[0].Value, @"[\-\+]?([\,]?(\d))"))
                str += match.Value;
            if (!double.TryParse(str, out latitude))
                return false;

            str = "";
            foreach (Match match in Regex.Matches(coordinates[1].Value, @"[\-\+]?([\,]?(\d))"))
                str += match.Value;
            if (!double.TryParse(str, out longitude))
                return false;

            latitude *= Regex.IsMatch(coordinates[0].Value, @"[Ss]") ? -1 : 1;
            longitude *= Regex.IsMatch(coordinates[1].Value, @"[Ww]") ? -1 : 1;

            position = new Position(latitude, longitude);

            return true;
        }

        public static string ToFormatString(this Position position)
        {
            var str = "";

            str += Math.Abs(position.Latitude);
            str += position.Latitude >= 0 ? "N" : "S";
            str += " ";
            str += Math.Abs(position.Longitude);
            str += position.Longitude >= 0 ? "E" : "W";

            return str;

        }

    }
}
