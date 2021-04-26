using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public class MapClickedEventArgsConverter : IValueConverter
    {
        #region ---IValueConverter Implementation---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mapClickedEventArgs = value as MapClickedEventArgs;
            if (mapClickedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type MapClickedEventArgs", nameof(value));
            }
            return mapClickedEventArgs.Point.Round();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
