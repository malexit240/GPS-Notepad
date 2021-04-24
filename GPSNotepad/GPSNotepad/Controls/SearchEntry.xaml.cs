using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPSNotepad.Controls
{
    public partial class SearchEntry : StackLayout
    {
        public SearchEntry()
        {
            InitializeComponent();
            searchEntry.Focused += SearchEntry_Focused;
            searchEntry.Unfocused += SearchEntry_Unfocused;
            searchEntry.TextChanged += SearchEntry_TextChanged;

        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public static BindableProperty SearchTextProperty = BindableProperty.Create(
            nameof(SearchText),
            typeof(string),
            typeof(DropDown),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnSearchTextPropertyChanged);

        private static void OnSearchTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SearchEntry).searchEntry.Text = newValue as string;
        }



        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = e.NewTextValue;
        }

        private void SearchEntry_Unfocused(object sender, FocusEventArgs e)
        {
            clearButton.IsVisible = false;
        }

        private void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            clearButton.IsVisible = true;
        }


        private void ClearButtonClicked(object sender, EventArgs e)
        {
            this.searchEntry.Text = string.Empty;
        }
    }
}