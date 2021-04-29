using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(GPSNotepad.Controls.NavigableTabbedPage), typeof(GPSNotepad.iOS.CustomTabbedPageRenderer))]
namespace GPSNotepad.iOS
{
    class CustomTabbedPageRenderer : TabbedRenderer
    {
        public UIImage imageWithColor(CGSize size, UIColor color)
        {

            var bottom = isOnce ? 0 : UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
            isOnce = true;

            size.Height += bottom;
            CGRect rect = new CGRect(0, -bottom, size.Width, size.Height);
            UIGraphics.BeginImageContext(size);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.SetFillColor(color.CGColor);
                context.FillRect(rect);
            }

            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }

        bool isOnce = false;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = this.Element as GPSNotepad.Controls.NavigableTabbedPage;

            var bottom = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
            CGSize size = new CGSize(TabBar.Frame.Width / TabBar.Items.Length, TabBar.Frame.Height);


            UITabBar.Appearance.SelectionIndicatorImage = imageWithColor(size, page.SelectedColor.ToUIColor());

        }
    }
}