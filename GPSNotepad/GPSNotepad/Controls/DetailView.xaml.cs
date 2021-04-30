using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class DetailView : StackLayout
    {
        #region ---Constructors---

        public DetailView() { }

        #endregion

        #region ---Public Properties---

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        #endregion

        #region ---Public Static Properties---

        public static new BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible),
            typeof(bool),
            typeof(DetailView),
            coerceValue: OnCoerceValue);

        #endregion


        #region ---Private Helpers---
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
        #endregion

        #region ---Event handlers---

        private static object OnCoerceValue(BindableObject bindable, object value)
        {
            var view = bindable as DetailView;

            view.AnimateVisibility((bool)value);

            return value;
        }

        #endregion

    }
}