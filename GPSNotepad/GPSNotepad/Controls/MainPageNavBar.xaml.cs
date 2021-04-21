using Prism.Commands;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public partial class MainPageNavBar : StackLayout
    {
        public MainPageNavBar()
        {
            InitializeComponent();
            this.searchField.BindingContext = this;

        }

        public bool IsFoccused
        {
            get => (bool)GetValue(IsFoccusedProperty);
            set => SetValue(IsFoccusedProperty, value);
        }

        public static BindableProperty IsFoccusedProperty = BindableProperty.Create(
            nameof(IsFoccused),
            typeof(bool),
            typeof(MainPageNavBar),
            defaultBindingMode: BindingMode.OneWayToSource);

        public ICommand SettingsButtonCommand
        {
            get => (ICommand)GetValue(SettingsButtonCommandProperty);
            set => SetValue(SettingsButtonCommandProperty, value);
        }

        public static BindableProperty SettingsButtonCommandProperty = BindableProperty.Create(
            nameof(SettingsButtonCommand),
            typeof(ICommand),
            typeof(MainPageNavBar));

        public ICommand OnSearchFieldFocused => new DelegateCommand(() =>
        {
            this.logoutButton.IsVisible = false;
            this.CancelButton.IsVisible = true;
            this.SettingsButton.IsVisible = false;
            IsFoccused = true;
        });
        public ICommand OnSearchFieldUnfocused => new DelegateCommand(() =>
        {
            this.logoutButton.IsVisible = true;
            this.CancelButton.IsVisible = false;
            this.SettingsButton.IsVisible = true;
            IsFoccused = false;
        });

        private void UnfocusSearchField(object sender, EventArgs e)
        {
            this.searchField.Unfocus();
        }
        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            SettingsButtonCommand?.Execute(null);
        }



    }
}