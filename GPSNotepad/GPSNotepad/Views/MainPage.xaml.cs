using Xamarin.Forms;

namespace GPSNotepad.Views
{

    public partial class MainPage : NavigableTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.SetBinding(ChoosenPageProperty, new Binding("ChoosenPage"));
        }
    }
}
