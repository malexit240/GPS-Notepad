using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        #region ---Constructors---

        public StartPageViewModel(INavigationService navigationService) : base(navigationService) { }

        #endregion

        #region ---Commands---

        private ICommand goToSignInPageCommand;
        public ICommand GoToSignInPageCommand => goToSignInPageCommand ??= new DelegateCommand(GoToSignInPageHandler);
        private async void GoToSignInPageHandler()
        {
            await NavigationService.NavigateAsync(nameof(SignInPage));
        }

        private ICommand goToSignUpPageCommand;
        public ICommand GoToSignUpPageCommand => goToSignUpPageCommand ??= new DelegateCommand(GoToSignUpPageHandler);
        private async void GoToSignUpPageHandler()
        {
            await NavigationService.NavigateAsync(nameof(SignUpOnePage));
        }

        #endregion
    }
}