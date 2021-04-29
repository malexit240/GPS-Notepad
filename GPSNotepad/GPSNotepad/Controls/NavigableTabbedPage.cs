using Xamarin.Forms;
using static Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page;
using static Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage;

namespace GPSNotepad.Controls
{
    public class NavigableTabbedPage : TabbedPage
    {
        #region ---Constructors---
        public NavigableTabbedPage()
        {
            this.CurrentPageChanged += OnCurrentPageChanged;
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
        }
        #endregion

        #region ---Public Properties
        public int ChoosenPage
        {
            get => (int)GetValue(ChoosenPageProperty);
            set => SetValue(ChoosenPageProperty, value);
        }
        #endregion

        #region ---Public Static Properties---
        public static BindableProperty ChoosenPageProperty = BindableProperty.Create(nameof(ChoosenPage),
            typeof(int),
            typeof(NavigableTabbedPage),
            defaultValue: 0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnChoosenPagePropertyChange);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }
        #endregion

        #region ---Public Static Properties---
        public static BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor),
            typeof(Color),
            typeof(NavigableTabbedPage));


        public  Color UnSelectedColor
        {
            get => (Color)GetValue(UnSelectedColorProperty);
            set => SetValue(UnSelectedColorProperty, value);
        }
        #endregion

        #region ---Public Static Properties---
        public static BindableProperty UnSelectedColorProperty = BindableProperty.Create(nameof(UnSelectedColor),
            typeof(Color),
            typeof(NavigableTabbedPage));


        #endregion

        #region ---Event Handlers---
        private void OnCurrentPageChanged(object sender, System.EventArgs e)
        {
            ChoosenPage = this.Children.IndexOf(this.CurrentPage);
        }

        private static void OnChoosenPagePropertyChange(BindableObject bindable, object oldValue, object newValue)
        {
            var page = (NavigableTabbedPage)bindable;
            var index = (int)newValue;

            if (page.Children.IndexOf(page.CurrentPage) != index)
            {
                page.CurrentPage = page.Children[index];
            }
        }
        #endregion
    }
}
