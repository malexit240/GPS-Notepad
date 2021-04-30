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
        #region ---Constructors---

        public SearchEntry()
        {
            InitializeComponent();
            searchEntry.Focused += SearchEntry_Focused;
            searchEntry.Unfocused += SearchEntry_Unfocused;
            searchEntry.TextChanged += SearchEntry_TextChanged;
        }

        #endregion

        #region ---Public Properties---

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        #endregion

        #region ---Public Static Properties---

        public static BindableProperty SearchTextProperty = BindableProperty.Create(
           nameof(SearchText),
           typeof(string),
           typeof(DropDown),
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: OnSearchTextPropertyChanged);

        #endregion


        #region ---Event Handlers---

        private static void OnSearchTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as SearchEntry).searchEntry.Text = newValue as string;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText = e.NewTextValue;
        }

        private async void SearchEntry_Unfocused(object sender, FocusEventArgs e)
        {
            await clearButton.ScaleTo(0);
            clearButton.IsVisible = false;
        }

        private async void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            clearButton.IsVisible = true;
            await clearButton.ScaleTo(1);
        }

        private void ClearButtonClicked(object sender, EventArgs e)
        {
            this.searchEntry.Text = string.Empty;
        }

        #endregion
    }
}