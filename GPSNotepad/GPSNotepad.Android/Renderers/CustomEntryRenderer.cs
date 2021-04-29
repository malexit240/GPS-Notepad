using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace GPSNotepad.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        #region ---Constructors---
        public CustomEntryRenderer(Context context) : base(context) { }
        #endregion

        #region ---Overrides---
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
            }
        }
        #endregion
    }
}

