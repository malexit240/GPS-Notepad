using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace GPSNotepad.Droid
{
    public class StyledNavigationRenderer : NavigationPageRenderer
    {
        #region ---Constructors---
        public StyledNavigationRenderer(Context context) : base(context) { }
        #endregion

        #region ---Overrides---
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            e.NewElement.Style = App.Current.Resources["navigationPageStyle"] as Style;
        }
        #endregion
    }

}

