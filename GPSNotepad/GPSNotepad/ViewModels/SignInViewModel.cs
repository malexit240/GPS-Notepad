using GPSNotepad.Model;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;
using Xamarin.Forms;
using System;

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
        #endregion

        #region ---Commands---
        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ??= new DelegateCommand(SignInCommandHandler, CanExecuteSignInCommand);
        private async void SignInCommandHandler()
        {
            await AuthenticationService.SignInAsync(Email, Password);

            if (AuthorizationService.IsAuthorized)
                await this.NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
        }
        private bool CanExecuteSignInCommand() => Email.Length != 0 && Password.Length != 0;



        private ICommand goToSignUpPageCommand;
        public ICommand GoToSignUpPageCommand => goToSignUpPageCommand ??= new DelegateCommand(GoToSignUpPageHandler);
        private async void GoToSignUpPageHandler()
        {
            await NavigationService.NavigateAsync(nameof(SignUpPage));
        }
        #endregion
    }
}
