using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void SearchEntry_Unfocused(object sender, FocusEventArgs e)
        {
            clearButton.IsVisible = false;
        }

        private void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            clearButton.IsVisible = true;
        }
    }
}