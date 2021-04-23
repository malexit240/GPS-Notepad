using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GPSNotepad.Controls
{
    public partial class DetailView : StackLayout
    {
        public DetailView()
        {
            //InitializeComponent();
        }

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public static new BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible),
            typeof(bool),
            typeof(DetailView),
            propertyChanged: OnIsVisiblePropertyChanged);

        private static void OnIsVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as DetailView;
            var value = (bool)newValue;

            if (value)
            {
                view.ScaleYTo(1, 100);
            }
            else
            {
                view.ScaleYTo(0, 100);
            }


        }
    }
}