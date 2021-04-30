using GPSNotepad.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class DropDown : StackLayout
    {
        #region ---Constructors---

        public DropDown()
        {
            InitializeComponent();
        }

        #endregion

        #region ---Public Properties---

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public ObservableCollection<PinViewModel> PinsSource
        {
            get => (ObservableCollection<PinViewModel>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public ICommand PinTappedCommand
        {
            get => (ICommand)GetValue(PinTappedCommandProperty);
            set => SetValue(PinTappedCommandProperty, value);
        }

        #endregion

        #region ---Public Static Proeprties---

        public static new BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible),
            typeof(bool),
            typeof(DropDown),
            coerceValue: OnCoerceValue);

        public static BindableProperty PinsSourceProperty = BindableProperty.Create(
            nameof(PinsSource),
            typeof(ObservableCollection<PinViewModel>),
            typeof(DropDown),
            propertyChanged: OnPinsSourcePropertyChanged);

        public static BindableProperty PinTappedCommandProperty = BindableProperty.Create(
            nameof(PinsSource),
            typeof(ICommand),
            typeof(DropDown));
        #endregion

        #region ---Event Handlers---

        private static void OnPinsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var dropDown = bindable as DropDown;
            var source = newValue as ObservableCollection<PinViewModel>;

            if (dropDown != null && source != null)
            {
                BindableLayout.SetItemsSource(dropDown.stackLayout, source);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PinTappedCommand?.Execute((sender as Grid).BindingContext);
        }

        private static object OnCoerceValue(BindableObject bindable, object value)
        {
            var view = bindable as DropDown;

            view.AnimateVisibility((bool)value);

            return value;
        }

        #endregion

        #region ---Private Helpers---

        private async void AnimateVisibility(bool visibility)
        {
            if (visibility == true)
            {
                base.IsVisible = visibility;
                await this.TranslateTo(0, 0);
                double height = 0;

                for (int i = 0; i < 2 && i < stackLayout.Children.Count; i++)
                {
                    height += stackLayout.Children[i].Height;
                }

                scrollView.HeightRequest = height >= 0 ? height : 0;
            }
            else
            {
                await this.TranslateTo(0, -Height);
                base.IsVisible = visibility;
            }
        }

        #endregion
    }
}