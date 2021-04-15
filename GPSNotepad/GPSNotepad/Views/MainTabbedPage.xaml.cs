using Xamarin.Forms;

namespace GPSNotepad.Views
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
