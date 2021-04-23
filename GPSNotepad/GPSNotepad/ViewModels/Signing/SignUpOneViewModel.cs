using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Enums;
using GPSNotepad.Views;

namespace GPSNotepad.ViewModels
{
    public class SignUpOneViewModel : ViewModelBase
    {
        #region ---Constructors---
        public SignUpOneViewModel(INavigationService navigationService, IAuthorizationService authorizationService) : base(navigationService)
        {

            AuthorizationService = authorizationService;

            var goToSignUpTwoPageCommand = GoToSignUpTwoPageCommand as DelegateCommand;
            goToSignUpTwoPageCommand.ObservesProperty(() => Email);
            goToSignUpTwoPageCommand.ObservesProperty(() => Name);
        }
        #endregion

        #region ---Protected Properties---
        protected IAuthorizationService AuthorizationService { get; set; }
        #endregion

        #region ---Public Properties---
        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isEmailWrong = false;
        public bool IsEmailWrong
        {
            get => _isEmailWrong;
            set => SetProperty(ref _isEmailWrong, value);
        }
        #endregion

        #region ---Commands---

        private ICommand _clearNameCommand;
        public ICommand ClearNameCommand => _clearNameCommand ??= new DelegateCommand(() => { Name = string.Empty; });

        private ICommand _clearEmailCommand;
        public ICommand ClearEmailCommand => _clearEmailCommand ??= new DelegateCommand(() => { Email = string.Empty; });


        private ICommand _goToSignUpTwoPageCommand;
        public ICommand GoToSignUpTwoPageCommand => _goToSignUpTwoPageCommand ??= new DelegateCommand(GoToSignUpTwoPage, canExecuteGoToSignUpTwoPage);
        private async void GoToSignUpTwoPage()
        {
            var emailValidationResult = Validators.Validators.IsEmailValid(_email);

            if (await AuthorizationService.IsUserExist(Email) ||
                emailValidationResult != EmailValidationStatus.Done)
            {
                IsEmailWrong = true;
            }
            else
            {
                IsEmailWrong = false;

                var parameters = new NavigationParameters()
                {
                    {nameof(Email), Email },
                    {nameof(Name), Name },
                };

                await NavigationService.NavigateAsync(nameof(SignUpTwoPage), parameters);
            }
        }
        private bool canExecuteGoToSignUpTwoPage() => Email.Length != 0 && Name.Length != 0;
        #endregion
    }
}

