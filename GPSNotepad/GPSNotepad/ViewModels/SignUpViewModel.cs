using Prism.Commands;
using Prism.Navigation;
using GPSNotepad.Validators;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;

namespace GPSNotepad.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region ---Public Properties---
        private string _email = "";
        private string _login = "";
        private string _password = "";
        private string _confirmPassword = "";
        private bool _hasLongActivity = false;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public bool HasLongActivity
        {
            get => _hasLongActivity;
            set => SetProperty(ref _hasLongActivity, value);
        }

        public ICommand SignUpCommand { get; private set; }
        #endregion

        #region ---Constructors---
        public SignUpViewModel(INavigationService navigationService, IAuthenticationService authorizationService) : base(navigationService)
        {
            SignUpCommand = new DelegateCommand(async () =>
            {
                bool done = true;
                HasLongActivity = true;
                if (Password != ConfirmPassword)
                {
                    done = false;
                }

                var emailValidationResult = await Validators.Validators.IsEmailValid(Email);
                var passwordValidationResult = Validators.Validators.IsPasswordValid(Password);

                switch (passwordValidationResult)
                {
                    case PasswordValidationStatus.InvalidContent:
                        break;
                    case PasswordValidationStatus.InvalidLength:
                        break;
                }

                switch (emailValidationResult)
                {
                    case EmailValidationStatus.InvalidFormat:
                        break;
                    case EmailValidationStatus.EmailAlreadyExist:
                        break;
                }

                done = emailValidationResult == EmailValidationStatus.Done && passwordValidationResult == PasswordValidationStatus.Done;

                if (!done)
                {
                    HasLongActivity = false;

                }
                else
                {
                    if (!await authorizationService.SignUpAsync(Email, Login, Password))
                        HasLongActivity = false;
                    else
                        await this.NavigationService.GoBackAsync();
                }
            }, canExecuteMethod: () => Email.Length != 0 && Password.Length != 0 && Login.Length != 0 && ConfirmPassword.Length != 0 && HasLongActivity != true);

            ((DelegateCommand)SignUpCommand).ObservesProperty(() => Email);
            ((DelegateCommand)SignUpCommand).ObservesProperty(() => Login);
            ((DelegateCommand)SignUpCommand).ObservesProperty(() => ConfirmPassword);
            ((DelegateCommand)SignUpCommand).ObservesProperty(() => Password);
            ((DelegateCommand)SignUpCommand).ObservesProperty(() => HasLongActivity);
        }
        #endregion
    }
}

