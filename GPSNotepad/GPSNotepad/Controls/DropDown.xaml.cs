using GPSNotepad.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class DropDown : StackLayout
    {
        public DropDown()
        {
            InitializeComponent();
        }

        public ObservableCollection<PinViewModel> PinsSource
        {
            get => (ObservableCollection<PinViewModel>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public static BindableProperty PinsSourceProperty = BindableProperty.Create(
            nameof(PinsSource),
            typeof(ObservableCollection<PinViewModel>),
            typeof(DropDown),
            propertyChanged: OnPinsSourcePropertyChanged);

        public ICommand PinTappedCommand
        {
            get => (ICommand)GetValue(PinTappedCommandProperty);
            set => SetValue(PinTappedCommandProperty, value);
        }

        public static BindableProperty PinTappedCommandProperty = BindableProperty.Create(
            nameof(PinsSource),
            typeof(ICommand),
            typeof(DropDown));

        private static void OnPinsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var dropDown = bindable as DropDown;
            var source = newValue as ObservableCollection<PinViewModel>;

            if (dropDown != null && source != null)
            {
                dropDown.listView.ItemsSource = source;
            }
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            PinTappedCommand?.Execute(e.Item);
        }
    }
}