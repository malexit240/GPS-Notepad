using GPSNotepad.Model;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.Authorization;

namespace GPSNotepad.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {

        #region ---Public Properties---
        private string _email = "";
        private string _password = "";

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SignInCommand { get; private set; }

        public DelegateCommand GoToSignUpPageCommand { get; set; }
        #endregion

        #region ---Constructors---
        public SignInViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IAuthorizationService authorizationService) : base(navigationService)
        {

            SignInCommand = new DelegateCommand(async () =>
            {
                await authenticationService.SignInAsync(Email, Password);

                if (authorizationService.IsAuthorized)
                    await this.NavigationService.NavigateAsync(nameof(MainTabbedPage));

            }, canExecuteMethod: () => Email.Length != 0 && Password.Length != 0);

            ((DelegateCommand)SignInCommand).ObservesProperty(() => Email);
            ((DelegateCommand)SignInCommand).ObservesProperty(() => Password);

            GoToSignUpPageCommand = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync(nameof(SignUpPage));
            });

        }
        #endregion
    }
}
