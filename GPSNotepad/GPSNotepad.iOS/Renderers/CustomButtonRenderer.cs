using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(GPSNotepad.iOS.CustomButtonRenderer))]
namespace GPSNotepad.iOS
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> args)
        {
            base.OnElementChanged(args);
            if (Control != null) SetColors();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);
            if (args.PropertyName == nameof(Button.IsEnabled)) SetColors();
        }

        private void SetColors()
        {
            Control.SetTitleColor(Element.TextColor.ToUIColor(), Element.IsEnabled ? UIControlState.Normal : UIControlState.Disabled);
        }
    }
}