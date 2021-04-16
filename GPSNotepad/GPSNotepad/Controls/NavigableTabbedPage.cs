using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class NavigableTabbedPage : TabbedPage
    {
        public NavigableTabbedPage()
        {
            this.CurrentPageChanged += OnCurrentPageChanged;
        }

        public int ChoosenPage
        {
            get => (int)GetValue(ChoosenPageProperty);
            set => SetValue(ChoosenPageProperty, value);
        }

        public BindableProperty ChoosenPageProperty = BindableProperty.Create(nameof(ChoosenPage),
            typeof(int),
            typeof(NavigableTabbedPage),
            defaultValue: 0,
            BindingMode.TwoWay,
            propertyChanged: OnChoosenPagePropertyChange);

        private void OnCurrentPageChanged(object sender, System.EventArgs e)
        {
            ChoosenPage = this.Children.IndexOf(this.CurrentPage);
        }

        private static void OnChoosenPagePropertyChange(BindableObject bindable, object oldValue, object newValue)
        {
            var page = (NavigableTabbedPage)bindable;
            var index = (int)newValue;

            if (page.Children.IndexOf(page.CurrentPage) == index)
                return;

            page.CurrentPage = page.Children[index];
        }
    }
}
