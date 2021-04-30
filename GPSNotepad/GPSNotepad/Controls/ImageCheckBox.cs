using System;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class ImageCheckBox : ImageButton
    {
        #region ---Constructors---

        public ImageCheckBox()
        {
            this.BackgroundColor = Color.Transparent;
            this.Clicked += OnImageCheckBoxClicked;
        }

        #endregion

        #region ---Public Properties---

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public ImageSource CheckedImageSource
        {
            get => (ImageSource)GetValue(CheckedImageSourceProperty);
            set => SetValue(CheckedImageSourceProperty, value);
        }

        public ImageSource UncheckedImageSource
        {
            get => (ImageSource)GetValue(UncheckedImageSourceProperty);
            set => SetValue(UncheckedImageSourceProperty, value);
        }

        #endregion

        #region ---Public Static Properties---

        public static BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
            typeof(bool),
            typeof(ImageCheckBox),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: IsCheckedPropertyChanged);

        public static BindableProperty CheckedImageSourceProperty = BindableProperty.Create(nameof(CheckedImageSource),
            typeof(ImageSource),
            typeof(ImageCheckBox),
            propertyChanged: OnCheckedImageSourcePropertyChanged);

        public static BindableProperty UncheckedImageSourceProperty = BindableProperty.Create(nameof(UncheckedImageSource),
            typeof(ImageSource),
            typeof(ImageCheckBox),
            propertyChanged: OnUncheckedImageSourcePropertyChanged);

        #endregion

        #region ---Event Handlers---

        private void OnImageCheckBoxClicked(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var imageCheckBox = bindable as ImageCheckBox;

            imageCheckBox?.ReSetSource();
        }

        private static void OnCheckedImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var imageCheckBox = bindable as ImageCheckBox;

            imageCheckBox?.ReSetSource();
        }

        private static void OnUncheckedImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var imageCheckBox = bindable as ImageCheckBox;

            imageCheckBox?.ReSetSource();
        }
        #endregion

        #region ---Private Helpers---
        private void ReSetSource()
        {
            this.Source = IsChecked && this.CheckedImageSource != null ? this.CheckedImageSource : this.UncheckedImageSource;
        }
        #endregion
    }
}
