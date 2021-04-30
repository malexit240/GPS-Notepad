using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class LabeledEntry : StackLayout
    {
        #region ---Constructors---

        public LabeledEntry() : base()
        {
            Label = new Label();
            WrongLabel = new Label();
            Entry = new Entry();
            Button = new ImageCheckBox();
            Frame = new Frame();

            WrongLabel.Text = "   ";

            Entry.Focused += OnEntryFocused;
            Entry.Unfocused += OnEntryUnfocused;
            Entry.TextChanged += OnEntryTextChanged;

            Button.Clicked += OnButtonClick;

            Button.IsVisible = false;


            Label.SetDynamicResource(StyleProperty, "entryLabelStyle");
            WrongLabel.SetDynamicResource(StyleProperty, "wrongEntryLabelStyle");
            Entry.SetDynamicResource(StyleProperty, "labeledEntryStyle");
            Button.SetDynamicResource(StyleProperty, "entryButtonStyle");
            Frame.SetDynamicResource(StyleProperty, "entryFrameStyle");

            var stack = new StackLayout();
            stack.Orientation = StackOrientation.Horizontal;
            Entry.HorizontalOptions = LayoutOptions.FillAndExpand;
            Button.HorizontalOptions = LayoutOptions.End;

            stack.Children.Add(Entry);
            stack.Children.Add(Button);

            Frame.Content = stack;

            this.Children.Add(Label);
            this.Children.Add(Frame);
            this.Children.Add(WrongLabel);

        }

        #endregion

        #region ---Protected Properties---

        protected ImageCheckBox Button { get; set; }
        protected Frame Frame { get; set; }
        protected Label Label { get; set; }
        protected Label WrongLabel { get; set; }
        protected Entry Entry { get; set; }

        #endregion

        #region ---Public Properties---

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public string WrongLabelText
        {
            get => (string)GetValue(WrongLabelTextProperty);
            set => SetValue(WrongLabelTextProperty, value);
        }

        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public string EntryPlaceholderText
        {
            get => (string)GetValue(EntryPlaceholderTextProperty);
            set => SetValue(EntryPlaceholderTextProperty, value);
        }

        public bool IsWrong
        {
            get => (bool)GetValue(IsWrongProperty);
            set => SetValue(IsWrongProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public ImageSource ButtonImageSource
        {
            get => (ImageSource)GetValue(ButtonImageSourceProperty);
            set => SetValue(ButtonImageSourceProperty, value);
        }

        public ImageSource ButtonCheckedImageSource
        {
            get => (ImageSource)GetValue(ButtonCheckedImageSourceProperty);
            set => SetValue(ButtonCheckedImageSourceProperty, value);
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        #endregion

        #region ---Public Static Properties---

        public static BindableProperty LabelTextProperty = BindableProperty.Create(
         nameof(LabelText),
         typeof(string),
         typeof(LabeledEntry),
         defaultValue: string.Empty,
         propertyChanged: OnLabelTextPropertyChanged);


        public static BindableProperty WrongLabelTextProperty = BindableProperty.Create(
            nameof(WrongLabelText),
            typeof(string),
            typeof(LabeledEntry),
            defaultValue: "    ",
            propertyChanged: OnWrongLabelTextPropertyChanged);

        public static BindableProperty EntryTextProperty = BindableProperty.Create(
            nameof(EntryText),
            typeof(string),
            typeof(LabeledEntry),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnEntyTextTextPropertyChanged);


        public static BindableProperty EntryPlaceholderTextProperty = BindableProperty.Create(
            nameof(EntryText),
            typeof(string),
            typeof(LabeledEntry),
            propertyChanged: OnEntryPlaceholderTextPropertyChanged);


        public static BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(LabeledEntry),
            defaultValue: false,
            coerceValue: OnIsPasswordPropertyChanged);


        public static BindableProperty IsWrongProperty = BindableProperty.Create(
            nameof(IsWrong),
            typeof(bool),
            typeof(LabeledEntry),
            defaultValue: false,
            coerceValue: OnIsWrongPropertyChanged);

        public static BindableProperty ButtonImageSourceProperty = BindableProperty.Create(
            nameof(ButtonImageSource),
            typeof(ImageSource),
            typeof(LabeledEntry),
            propertyChanged: ButtonImageSourcePropertyChanged);


        public static BindableProperty ButtonCheckedImageSourceProperty = BindableProperty.Create(
         nameof(ButtonImageSource),
         typeof(ImageSource),
         typeof(LabeledEntry),
         propertyChanged: OnButtonCheckedImageSourcePropertyChanged);


        public static BindableProperty ButtonCommandProperty = BindableProperty.Create(
          nameof(ButtonImageSource),
          typeof(ICommand),
          typeof(LabeledEntry));

        #endregion

        #region ---Event Handlers---

        private static void OnLabelTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var labeledEntry = bindable as LabeledEntry;
            var text = newValue as string;

            if (labeledEntry != null && text != null)
            {
                labeledEntry.Label.Text = text;
            }
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            Button.IsVisible = true;
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            Button.IsVisible = false;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != e.NewTextValue)
                EntryText = e.NewTextValue;
        }

        private static void OnEntyTextTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var text = newValue as string;

            if (LabeledEntry != null && text != null && LabeledEntry.Entry.Text != text)
            {
                LabeledEntry.Entry.Text = text;
            }
        }

        private static void OnWrongLabelTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var labeledEntry = bindable as LabeledEntry;
            var text = newValue as string;

            if (labeledEntry != null && text != null)
            {
                if (labeledEntry.IsWrong)
                {
                    labeledEntry.WrongLabel.Text = text;
                }
            }
        }

        private static void OnEntryPlaceholderTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var text = newValue as string;

            if (LabeledEntry != null && text != null && LabeledEntry.Entry.Placeholder != text)
            {
                LabeledEntry.Entry.Placeholder = text;
            }
        }

        private static object OnIsPasswordPropertyChanged(BindableObject bindable, object Value)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var isPassword = (bool)Value;

            if (LabeledEntry != null && LabeledEntry.Entry.IsPassword != isPassword)
            {
                LabeledEntry.Entry.IsPassword = isPassword;
            }

            return Value;
        }

        private static object OnIsWrongPropertyChanged(BindableObject bindable, object value)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var isWrong = (bool)value;

            if (LabeledEntry != null)
            {
                if (isWrong)
                {
                    LabeledEntry.Frame.SetDynamicResource(StyleProperty, "entryWrongFrameStyle");
                    LabeledEntry.WrongLabel.Text = LabeledEntry.WrongLabelText;
                }
                else
                {
                    LabeledEntry.Frame.SetDynamicResource(StyleProperty, "entryFrameStyle");
                    LabeledEntry.WrongLabel.Text = "     ";
                }

            }

            return value;
        }


        private static void ButtonImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var value = newValue as ImageSource;

            if (LabeledEntry != null && value != null)
            {
                LabeledEntry.Button.UncheckedImageSource = value;
            }
        }


        private static void OnButtonCheckedImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var LabeledEntry = bindable as LabeledEntry;
            var value = newValue as ImageSource;

            if (LabeledEntry != null && value != null)
            {
                LabeledEntry.Button.CheckedImageSource = value;
            }
        }


        private void OnButtonClick(object sender, EventArgs e)
        {
            ButtonCommand?.Execute(null);
        }

        #endregion
    }
}
