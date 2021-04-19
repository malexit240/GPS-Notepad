using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Enums;

namespace GPSNotepad.Temp.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region ---Constructors---
        public SignUpViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService) : base(navigationService)
        {
            AuthorizationService = authorizationService;
            AuthenticationService = authenticationService;

            var signUpCommand = SignUpCommand as DelegateCommand;
            signUpCommand.ObservesProperty(() => Email);
            signUpCommand.ObservesProperty(() => Login);
            signUpCommand.ObservesProperty(() => ConfirmPassword);
            signUpCommand.ObservesProperty(() => Password);
            signUpCommand.ObservesProperty(() => HasLongActivity);
        }
        #endregion

        #region ---Protected Properties---
        protected IAuthorizationService AuthorizationService { get; set; }
        protected IAuthenticationService AuthenticationService { get; set; }
        #endregion

        #region ---Public Properties---
        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _login = "";
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword = "";
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _hasLongActivity = false;
        public bool HasLongActivity
        {
            get => _hasLongActivity;
            set => SetProperty(ref _hasLongActivity, value);
        }

        #endregion

        #region ---Commands---
        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new DelegateCommand(SignUpCommandHandler, canExecuteSignUpCommand);
        private async void SignUpCommandHandler()
        {
            bool done = true;
            HasLongActivity = true;

            if (Password != ConfirmPassword)
            {
                done = false;
            }

            var emailValidationResult = Validators.Validators.IsEmailValid(Email);
            var passwordValidationResult = Validators.Validators.IsPasswordValid(Password);

            switch (passwordValidationResult)
            {
                case PasswordValidationStatus.InvalidContent:
                    Acr.UserDialogs.UserDialogs.Instance.Alert(TextResources["InvalidPasswordContentMessage"], TextResources["InvalidPasswordContent"]);
                    break;
                case PasswordValidationStatus.InvalidLength:
                    Acr.UserDialogs.UserDialogs.Instance.Alert(TextResources["InvalidPasswordLengthMessage"], TextResources["InvalidPasswordLength"]);
                    break;
            }

            switch (emailValidationResult)
            {
                case EmailValidationStatus.InvalidFormat:
                    Acr.UserDialogs.UserDialogs.Instance.Alert(TextResources["InvalidEmailFormatMessage"], TextResources["InvalidEmailFormat"]);
                    break;
            }

            done = emailValidationResult == EmailValidationStatus.Done && passwordValidationResult == PasswordValidationStatus.Done;

            if (!done)
            {
                HasLongActivity = false;
            }
            else
            {
                if (await AuthorizationService.IsUserExist(Email))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(TextResources["UseAlreadyExistMessage"], TextResources["UserAlreadyExist"]);
                }

                if (!await AuthenticationService.SignUpAsync(Email, Login, Password))
                {
                    HasLongActivity = false;
                }
                else
                {
                    await this.NavigationService.GoBackAsync();
                }
            }
        }
        private bool canExecuteSignUpCommand() => Email.Length != 0 && Password.Length != 0 && Login.Length != 0 && ConfirmPassword.Length != 0 && HasLongActivity != true;
        #endregion
    }
}

