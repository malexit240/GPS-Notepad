using GPSNotepad.Model;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

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
        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
            SignInCommand = new DelegateCommand(async () =>
            {
                await Authorizator.AutorizeAsync(Email, Password);
                if (CurrentUser.Instance != null)
                    await this.NavigationService.NavigateAsync(nameof(MainPage));
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
