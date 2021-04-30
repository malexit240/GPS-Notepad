using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class PageNavbar : Grid
    {
        #region ---Constructors---

        public PageNavbar()
        {
            InitializeComponent();
        }

        #endregion

        #region ---Public Properties---

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public bool IsBackButtonEnabled
        {
            get => (bool)GetValue(IsBackButtonEnabledProperty);
            set => SetValue(IsBackButtonEnabledProperty, value);
        }

        #endregion

        #region ---Public Static Properties---

        public static BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(PageNavbar),
            coerceValue: OnTitlePropertyChanged);

        public static BindableProperty IsBackButtonEnabledProperty = BindableProperty.Create(
            nameof(IsBackButtonEnabled),
            typeof(bool),
            typeof(PageNavbar),
            true,
            coerceValue: OnIsBackButtonEnabledPropertyChanged);

        #endregion

        #region ---Event Handlers---

        private static object OnIsBackButtonEnabledPropertyChanged(BindableObject bindable, object value)
        {

            var newValue = (bool)value;
            var navbar = (PageNavbar)bindable;

            if (newValue == true)
            {
                navbar.backButton.IsEnabled = true;
                navbar.backButton.Opacity = 0;
            }

            return value;
        }

        private static object OnTitlePropertyChanged(BindableObject bindable, object value)
        {
            var navbar = bindable as PageNavbar;

            if (navbar != null)
            {
                navbar.titleLabel.Text = (string)value;
            }

            return value;
        }

        #endregion
    }
}