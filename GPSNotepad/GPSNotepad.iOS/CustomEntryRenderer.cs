using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace GPSNotepad.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer() : base()
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;

            }
        }
    }
}

