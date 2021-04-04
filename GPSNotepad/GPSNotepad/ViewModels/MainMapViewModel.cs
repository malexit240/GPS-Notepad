using GPSNotepad.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms.GoogleMaps;

namespace GPSNotepad.ViewModels
{
    public class MainMapViewModel : ViewModelBase
    {
        private MapSpan _span;

        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        

        public MainMapViewModel(INavigationService navigationService) : base(navigationService)
        {
            CurrentPosition.GetAsync().ContinueWith(
                result => _span = new MapSpan(result.Result, 0.01, 0.01));

            

        }
    }
}
