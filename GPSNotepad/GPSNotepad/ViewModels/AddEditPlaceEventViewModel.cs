using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GPSNotepad.Services.PlaceEventsService;
using GPSNotepad.Extensions;

namespace GPSNotepad.ViewModels
{
    public class AddEditPlaceEventViewModel : ViewModelBase
    {

        private TimeSpan _time;
        public TimeSpan Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public PlaceEventViewModel PlaceEventViewModel { get; set; } = null;

        public ICommand AddEditPlaceEventCommand { get; set; }


        public bool IsEdit { get; set; }

        public AddEditPlaceEventViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddEditPlaceEventCommand = new DelegateCommand(async () =>
            {
                PlaceEventViewModel.Time = Date.Add(Time);
                PlaceEventViewModel.Description = Description;
                await navigationService.GoBackAsync((nameof(PlaceEventViewModel), this.PlaceEventViewModel));
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(PlaceEventViewModel)))
            {
                IsEdit = true;
                PlaceEventViewModel = parameters[nameof(PlaceEventViewModel)] as PlaceEventViewModel;
            }
            else
            {
                var pinId = parameters.GetValue<Guid>(nameof(PlaceEventViewModel.PinId));

                PlaceEventViewModel = new PlaceEventViewModel(Guid.NewGuid(), pinId, DateTime.Now, "");
            }

            Description = PlaceEventViewModel.Description;
            Date = PlaceEventViewModel.Time.Date;
            Time = PlaceEventViewModel.Time.TimeOfDay;
        }

    }
}
