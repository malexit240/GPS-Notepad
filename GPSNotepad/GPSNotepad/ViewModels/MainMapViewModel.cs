using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPSNotepad.ViewModels
{
    public class MainMapViewModel : ViewModelBase
    {
        public MainMapViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
