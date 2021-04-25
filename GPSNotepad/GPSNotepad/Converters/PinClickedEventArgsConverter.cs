using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public class PinTappedEventArgsConverter : IValueConverter
    {
        #region ---IValueConverter Implementation---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pinTappedEventArgs = value as PinTappedEventArgs;
            if (pinTappedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type PinTappedEventArgs", nameof(value));
            }
            return pinTappedEventArgs.Pin;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
