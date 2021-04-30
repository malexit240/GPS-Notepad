using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class AddEditPlaceEventViewModel : ViewModelBase
    {
        #region ---Constructors---

        public AddEditPlaceEventViewModel(INavigationService navigationService) : base(navigationService)
        {
            _time = DateTime.Now.TimeOfDay;
            _date = DateTime.Now.Date;
        }

        #endregion

        #region ---Public Properties---

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

        private string _description = "";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public PlaceEventViewModel PlaceEventViewModel { get; set; } = null;

        public bool IsEdit { get; set; }

        #endregion

        #region ---Commands---

        private ICommand _addEditPlaceEventCommand;
        public ICommand AddEditPlaceEventCommand => _addEditPlaceEventCommand ??=
        new DelegateCommand(AddEditPlaceEventHandler);

        #endregion


        #region ---Overrides---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(PlaceEventViewModel))) //On edit event
            {
                IsEdit = true;
                PlaceEventViewModel = parameters[nameof(PlaceEventViewModel)] as PlaceEventViewModel;
            }
            else                                                     //On create event
            {
                var pinId = parameters.GetValue<Guid>(nameof(PlaceEventViewModel.PinId));

                PlaceEventViewModel = new PlaceEventViewModel(Guid.NewGuid(), pinId, DateTime.Now, "");
            }

            Description = PlaceEventViewModel.Description;
            Date = PlaceEventViewModel.Time.Date;
            Time = PlaceEventViewModel.Time.TimeOfDay;
        }

        #endregion

        #region ---Private Helpers---

        private async void AddEditPlaceEventHandler()
        {
            PlaceEventViewModel.Time = Date.Add(Time);
            PlaceEventViewModel.Description = Description;

            await NavigationService.GoBackAsync((nameof(PlaceEventViewModel), this.PlaceEventViewModel));
        }

        #endregion
    }
}