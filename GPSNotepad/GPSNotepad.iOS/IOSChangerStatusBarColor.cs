using GPSNotepad.PlatformDependencyInterfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(GPSNotepad.iOS.IOSChangerStatusBarColor))]
namespace GPSNotepad.iOS
{
    public class IOSChangerStatusBarColor : IChangerBarColor
    {
        #region ---IChangerBarColor Implementation---
        public void SetBarColor(Color color)
        {


        }
        #endregion
    }
}