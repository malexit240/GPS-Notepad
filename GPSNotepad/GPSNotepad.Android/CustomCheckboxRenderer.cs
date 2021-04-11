using Android.Content;
using Android.Graphics;
using GPSNotepad.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace GPSNotepad.Droid
{
    public class CustomCheckboxRenderer : CheckBoxRenderer
    {
        public CustomCheckboxRenderer(Context context) : base(context) { }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

        }
    }
}

