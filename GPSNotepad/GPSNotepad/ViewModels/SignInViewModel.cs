using GPSNotepad.Model;
using GPSNotepad.Validators;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {

        private string _email;
        private string _password;

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

        public ICommand GoToSignUpPageCommand { get; private set; }

        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
            SignInCommand = new DelegateCommand(async () =>
            {
                Authorizator.AutorizeAsync(Email, Password);
                if (CurrentUser.Instance != null)
                    await this.NavigationService.NavigateAsync(nameof(MainPage));
            }, canExecuteMethod: () => Email.Length != 0 && Password.Length != 0);

            ((DelegateCommand)SignInCommand).ObservesProperty(() => Email);
            ((DelegateCommand)SignInCommand).ObservesProperty(() => Password);

            GoToSignUpPageCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync(nameof(SignUpPage));
            });

        }
    }
}
