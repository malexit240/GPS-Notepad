using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPSNotepad.ViewModels
{
    public class PinsViewModel : ViewModelBase
    {
        public PinsViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
