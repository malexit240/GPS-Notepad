using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPSNotepad.Controls
{
    public partial class DropDown : StackLayout
    {
        public DropDown()
        {
            InitializeComponent();
            Elems = new ObservableCollection<Elem>()
            {
                {new Elem(){Text = "123"} },
                {new Elem(){Text = "AAA"} },
                {new Elem(){Text = "12AA"} }
            };
            listView.ItemsSource = Elems;
        }

        public ObservableCollection<Elem> Elems { get; set; }


        public class Elem
        {
            public string Text { get; set; } = "123";
        }
    }
}