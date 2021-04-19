using GPSNotepad.Controls;
using Xamarin.Forms;

namespace GPSNotepad.Temp.Views
{
    public partial class MainTabbedPage : NavigableTabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
            this.SetBinding(ChoosenPageProperty, new Binding("ChoosenPage"));
        }
    }
}
