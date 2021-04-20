using GPSNotepad.Model;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using Xamarin.Forms;
using System;
using GPSNotepad.Views;

namespace GPSNotepad.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        #region ---Constructors---
        public SignInViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService) : base(navigationService)
        {

            AuthenticationService = authenticationService;
            AuthorizationService = authorizationService;

            var signInCommand = SignInCommand as DelegateCommand;
            signInCommand.ObservesProperty(() => Email);
            signInCommand.ObservesProperty(() => Password);
        }
        #endregion

        #region ---Protected Properties---
        protected IAuthenticationService AuthenticationService { get; set; }
        protected IAuthorizationService AuthorizationService { get; set; }
        #endregion

        #region ---Public Properties---

        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isHidePassword = true;
        public bool IsHidePassword
        {
            get => _isHidePassword;
            set => SetProperty(ref _isHidePassword, value);
        }

        private bool _isEmailWrong = false;
        public bool IsEmailWrong
        {
            get => _isEmailWrong;
            set => SetProperty(ref _isEmailWrong, value);
        }

        private bool _isPasswordWrong = false;
        public bool IsPasswordWrong
        {
            get => _isPasswordWrong;
            set => SetProperty(ref _isPasswordWrong, value);
        }

        #endregion

        #region ---Commands---
        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ??= new DelegateCommand(SignInCommandHandler, CanExecuteSignInCommand);
        private async void SignInCommandHandler()
        {
            await AuthenticationService.SignInAsync(Email, Password);

            if (!AuthorizationService.IsAuthorized)
            {
                IsEmailWrong = true;
                IsPasswordWrong = true;
            }
            else
            {
                //  await this.NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
            }
        }
        private bool CanExecuteSignInCommand() => Email.Length != 0 && Password.Length != 0;


        private ICommand _clearEmailCommand;
        public ICommand CearEmailCommand => _clearEmailCommand ??= new DelegateCommand(() => Email = string.Empty);


        private ICommand _showPasswordCommand;
        public ICommand ShowPasswordCommand => _showPasswordCommand ??= new DelegateCommand(ShowPasswordCommandHandler);
        private void ShowPasswordCommandHandler()
        {
            IsHidePassword = !IsHidePassword;
        }

        #endregion
    }
}
