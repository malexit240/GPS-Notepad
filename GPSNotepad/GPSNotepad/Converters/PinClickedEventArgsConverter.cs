using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public class PinClickedEventArgsConverter : IValueConverter
    {
        #region ---IValueConverter Implementation---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pinClickedEventArgs = value as PinClickedEventArgs;
            if (pinClickedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type PinClickedEventArgs", nameof(value));
            }
            return pinClickedEventArgs.Pin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
