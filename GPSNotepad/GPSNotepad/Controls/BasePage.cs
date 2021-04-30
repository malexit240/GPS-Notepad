using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GPSNotepad.Controls
{
    public class BasePage : ContentPage
    {
        #region ---Constructors---

        public BasePage() : base()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        #endregion
    }
}

