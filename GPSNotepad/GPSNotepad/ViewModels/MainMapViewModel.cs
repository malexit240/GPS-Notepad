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
        public MainMapViewModel(INavigationService navigationService) : base(navigationService)
        {
            CurrentPosition.GetAsync().ContinueWith(
                result => _span = new MapSpan(result.Result, 0.01, 0.01));



        }

        private MapSpan _span;
        public MapSpan Span
        {
            get => _span;
            set => SetProperty(ref _span, value);
        }

        private bool _showDetailView = false;
        public bool ShowDetailView
        {
            get => _showDetailView;
            set
            {
                SetProperty(ref _showDetailView, value);
                RaisePropertyChanged(nameof(ShowMoveToMyLocationButton));
            }
        }

        public bool ShowMoveToMyLocationButton => !_showDetailView;


        private ICommand _moveToMyLocationCommand;
        public ICommand MoveToMyLocationCommand => _moveToMyLocationCommand ??= new DelegateCommand(MoveToMyLocationHandler);
        private void MoveToMyLocationHandler()
        {
            CurrentPosition.GetAsync().ContinueWith(result => Span = new MapSpan(result.Result, 0.01, 0.01));
        }

    }
}
