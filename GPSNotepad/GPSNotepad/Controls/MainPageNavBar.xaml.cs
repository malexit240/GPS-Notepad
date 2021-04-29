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
            this.searchField.SetBinding(SearchEntry.SearchTextProperty, new Binding(nameof(SearchText)));
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

        public ICommand LogoutButtonCommand
        {
            get => (ICommand)GetValue(LogoutButtonCommandProperty);
            set => SetValue(LogoutButtonCommandProperty, value);
        }

        public static BindableProperty LogoutButtonCommandProperty = BindableProperty.Create(
            nameof(LogoutButtonCommand),
            typeof(ICommand),
            typeof(MainPageNavBar));


        public string SearchPlaceholderText
        {
            get => (string)GetValue(SearchPlaceholderTextProperty);
            set => SetValue(SearchPlaceholderTextProperty, value);
        }

        public static BindableProperty SearchPlaceholderTextProperty = BindableProperty.Create(
            nameof(SearchPlaceholderText),
            typeof(string),
            typeof(MainPageNavBar),
            defaultBindingMode: BindingMode.TwoWay);



        public ICommand OnSearchFieldFocused => new DelegateCommand(async () =>
        {
            this.CancelButton.IsVisible = true;
            this.SettingsButton.IsVisible = false;
            IsFoccused = true;
            searchField.LayoutTo(new Rectangle(searchField.X, searchField.Y, searchField.Width + logoutButton.Width * 1.05, searchField.Height));
            await logoutButton.ScaleTo(0);
        });



        public ICommand OnSearchFieldUnfocused => new DelegateCommand(async () =>
       {
           this.CancelButton.IsVisible = false;
           this.SettingsButton.IsVisible = true;

           IsFoccused = false;
           logoutButton.ScaleTo(1);
           await searchField.LayoutTo(new Rectangle(searchField.X, searchField.Y, searchField.Width - logoutButton.Width * 1.05, searchField.Height));

       });

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public static BindableProperty SearchTextProperty = BindableProperty.Create(
            nameof(SearchText),
            typeof(string),
            typeof(MainPageNavBar),
            defaultBindingMode: BindingMode.TwoWay);


        private void UnfocusSearchField(object sender, EventArgs e)
        {
            this.searchField.IsVisible = false;
            this.searchField.IsVisible = true;
        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            SettingsButtonCommand?.Execute(null);
        }

        private void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            LogoutButtonCommand?.Execute(null);
        }
    }
}