using System;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class ImageCheckBox : ImageButton
    {
        public ImageCheckBox()
        {
            this.BackgroundColor = Color.Transparent;
            this.Clicked += OnImageCheckBoxClicked;
        }

        public void ReSetSource()
        {
            this.Source = IsChecked ? this.CheckedImageSource : this.UncheckedImageSource;
        }


        private void OnImageCheckBoxClicked(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);

        }
        public static BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
            typeof(bool),
            typeof(ImageCheckBox),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: IsCheckedPropertyChanged);

        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var self = bindable as ImageCheckBox;

            self.ReSetSource();
        }


        public ImageSource CheckedImageSource
        {
            get => (ImageSource)GetValue(CheckedImageSourceProperty);
            set => SetValue(CheckedImageSourceProperty, value);
        }
        public static BindableProperty CheckedImageSourceProperty = BindableProperty.Create(nameof(CheckedImageSource),
            typeof(ImageSource),
            typeof(ImageCheckBox),
            propertyChanged: OnCheckedImageSourcePropertyChanged);

        private static void OnCheckedImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var self = bindable as ImageCheckBox;
            self.ReSetSource();
        }

        public ImageSource UncheckedImageSource
        {
            get => (ImageSource)GetValue(UncheckedImageSourceProperty);
            set => SetValue(UncheckedImageSourceProperty, value);
        }
        public static BindableProperty UncheckedImageSourceProperty = BindableProperty.Create(nameof(UncheckedImageSource),
            typeof(ImageSource),
            typeof(ImageCheckBox),
            propertyChanged: OnUncheckedImageSourcePropertyChanged);

        private static void OnUncheckedImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var self = bindable as ImageCheckBox;
            self.ReSetSource();
        }
    }
}
