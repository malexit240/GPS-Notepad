using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps.Clustering.iOS;
using Xamarin.Forms.GoogleMaps.iOS;

[assembly: ExportRenderer(typeof(GPSNotepad.Controls.BindableMap), typeof(GPSNotepad.iOS.BindableMapRenderer))]
namespace GPSNotepad.iOS
{
    public class BindableMapRenderer : ClusteredMapRenderer
    {
        public BindableMapRenderer() : base()
        {

        }



    }
}