using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.Converters
{
    public class CameraMovingEventArgsConverter : IValueConverter
    {
        #region ---IValueConverter Implementation---
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cameraMovingEventArgs = value as CameraMovingEventArgs;
            if (cameraMovingEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type CameraMovingEventArgs", nameof(value));
            }
            return cameraMovingEventArgs.Position.Target;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
