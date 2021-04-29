using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Enums;
using GPSNotepad.Views;
using Xamarin.Forms;

namespace GPSNotepad.ViewModels
{
    public class SignUpTwoViewModel : ViewModelBase
    {
        #region ---Constructors---
        public SignUpTwoViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService) : base(navigationService)
        {
            AuthorizationService = authorizationService;
            AuthenticationService = authenticationService;

            var signUpCommand = SignUpCommand as DelegateCommand;
            signUpCommand.ObservesProperty(() => ConfirmPassword);
            signUpCommand.ObservesProperty(() => Password);
        }
        #endregion

        #region ---Protected Properties---
        protected IAuthorizationService AuthorizationService { get; set; }
        protected IAuthenticationService AuthenticationService { get; set; }
        #endregion

        #region ---Public Properties---
        private string _email = "";
        private string _name = "";

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


        private bool _isHidePassword = true;
        public bool IsHidePassword
        {
            get => _isHidePassword;
            set => SetProperty(ref _isHidePassword, value);
        }

        private bool _isHideConfirmPassword = true;
        public bool IsHideConfirmPassword
        {
            get => _isHideConfirmPassword;
            set => SetProperty(ref _isHideConfirmPassword, value);
        }

        private string _wrongMessage = "";
        public string WrongMessage
        {
            get => _wrongMessage;
            set => SetProperty(ref _wrongMessage, value);
        }

        private bool _isPasswordWrong = false;
        public bool IsPasswordWrong
        {
            get => _isPasswordWrong;
            set => SetProperty(ref _isPasswordWrong, value);
        }

        #endregion

        #region ---Commands---

        private ICommand _showPasswordCommand;
        public ICommand ShowPasswordCommand => _showPasswordCommand ??= new DelegateCommand(() =>
        {
            IsHidePassword = !IsHidePassword;
        });


        private ICommand _showConfirmPasswordCommand;
        public ICommand ShowConfirmPasswordCommand => _showConfirmPasswordCommand ??= new DelegateCommand(() =>
        {
            IsHideConfirmPassword = !IsHideConfirmPassword;
        });


        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new DelegateCommand(SignUpCommandHandler, canExecuteSignUpCommand);
        private async void SignUpCommandHandler()
        {

            if (Password != ConfirmPassword)
            {
                IsPasswordWrong = true;
                WrongMessage = TextResources["PasswordMismatch"];
            }
            else
            {
                var passwordValidationResult = Validators.Validators.IsPasswordValid(Password);

                switch (passwordValidationResult)
                {
                    case PasswordValidationStatus.InvalidContent:
                        IsPasswordWrong = true;
                        WrongMessage = TextResources["IncorrectPasswordContent"];
                        break;
                    case PasswordValidationStatus.InvalidLength:
                        IsPasswordWrong = true;
                        WrongMessage = TextResources["IncorrectPasswordLength"];
                        break;
                }

                if (passwordValidationResult == PasswordValidationStatus.Done)
                {
                    if (await AuthenticationService.SignUpAsync(_email, _name, Password))
                    {
                        await this.NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(StartPage)}/{nameof(SignInPage)}");
                    }
                }
            }

        }
        private bool canExecuteSignUpCommand() => Password.Length != 0 && ConfirmPassword.Length != 0;
        #endregion


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(SignUpOneViewModel.Email)))
            {
                this._email = (string)parameters[nameof(SignUpOneViewModel.Email)];
            }

            if (parameters.ContainsKey(nameof(SignUpOneViewModel.Name)))
            {
                this._name = (string)parameters[nameof(SignUpOneViewModel.Name)];
            }

        }

    }
}

