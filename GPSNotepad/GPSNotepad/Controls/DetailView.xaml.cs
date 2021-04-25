using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class DetailView : StackLayout
    {
        public DetailView()
        {
        }

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public static new BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible),
            typeof(bool),
            typeof(DetailView),
            coerceValue: OnCoerceValue);

        private static object OnCoerceValue(BindableObject bindable, object value)
        {
            var view = bindable as DetailView;

            view.AnimateVisibility((bool)value);

            return value;
        }

        private async void AnimateVisibility(bool visibility)
        {
            if (visibility == true)
            {
                base.IsVisible = visibility;
                await this.TranslateTo(0, 0);
            }
            else
            {
                await this.TranslateTo(0, Height);
                base.IsVisible = visibility;
            }


        }
    }
}