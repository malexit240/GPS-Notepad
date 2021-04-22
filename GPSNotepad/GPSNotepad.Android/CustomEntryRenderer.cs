using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace GPSNotepad.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //Configure native control (TextBox)
            if (Control != null)
            {
                Control.Background = null;
            }
        }
    }
}

