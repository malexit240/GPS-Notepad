using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public class ShowDetaiPinViewArgsConverter : IValueConverter
    {
        #region ---IValueConverter Implementation---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pin = value as Pin;
            if (pin == null)
            {
                throw new ArgumentException("Expected value to be of type PinClickedEventArgs", nameof(value));
            }
            return pin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
