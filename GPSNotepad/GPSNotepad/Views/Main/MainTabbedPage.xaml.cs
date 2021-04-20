using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GPSNotepad.Views
{
    public partial class MainTabbedPage : GPSNotepad.Controls.NavigableTabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
            this.SetBinding(ChoosenPageProperty, new Binding("ChoosenPage"));
        }
    }
}