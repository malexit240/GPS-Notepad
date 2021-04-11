using Xamarin.Forms;
using System.Windows.Input;

namespace GPSNotepad.Controls
{
    public class CustomRadioButton : ContentView
    {
        protected StackLayout Stack { get; set; }
        public RadioButton Button { get; set; }
        public Label Label { get; set; }

        public CustomRadioButton() : base()
        {
            Stack = new StackLayout();
            Stack.Orientation = StackOrientation.Horizontal;

            Button = new RadioButton();
            Button.CheckedChanged += Button_CheckedChanged;


            Label = new Label();

            Stack.Children.Add(Button);
            Stack.Children.Add(Label);

            this.Content = Stack;

        }

        private void Button_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            this.ClickCommand?.Execute(null);
        }

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }
        public static BindableProperty ClickCommandProperty = BindableProperty.Create(nameof(ClickCommand),
            typeof(ICommand),
            typeof(CustomRadioButton));

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
        public static BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
            typeof(bool),
            typeof(CustomRadioButton),
            propertyChanged: IsCheckedPropertyChanged);
        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomRadioButton).Button.IsChecked = (bool)newValue;

        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(CustomRadioButton),
            propertyChanged: OnTextPropertyChanged);
        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomRadioButton).Label.Text = (string)newValue;

        }


        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }
        public static BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName),
            typeof(string),
            typeof(CustomRadioButton),
            propertyChanged: OnGroupNamePropertyChanged);
        private static void OnGroupNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomRadioButton).Button.GroupName = (string)newValue;
        }

    }
}
