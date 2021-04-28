using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPSNotepad.Controls
{
    public partial class PageNavbar : Grid
    {
        public PageNavbar()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(PageNavbar),
            coerceValue: OnTitlePropertyChanged);

        private static object OnTitlePropertyChanged(BindableObject bindable, object value)
        {
            var navbar = bindable as PageNavbar;

            if (navbar != null)
            {
                navbar.titleLabel.Text = (string)value;
            }

            return value;
        }
    }
}