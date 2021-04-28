using Android.Content;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace GPSNotepad.Droid
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        #region ---Constructors---
        public CustomButtonRenderer(Context context) : base(context) { }
        #endregion

        protected override void OnElementChanged(ElementChangedEventArgs<Button> args)
        {
            base.OnElementChanged(args);
            if (Control != null) SetColors();
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Button.IsEnabled)) SetColors();
        }

        private void SetColors()
        {
            Control.SetTextColor(this.Element.TextColor.ToAndroid());
            Control.SetAllCaps(false);
        }
    }
}

