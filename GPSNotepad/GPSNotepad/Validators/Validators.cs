using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GPSNotepad.Model.Interfaces;
using System.Linq;

namespace GPSNotepad.Validators
{
    public static class Validators
    {
        public static async Task<EmailValidationStatus> IsEmailValid(string email)
        {

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                return EmailValidationStatus.InvalidFormat;
            }

            if (await App.Current.Container.Resolve<IAuthorizatorService>().IsUserExist(email))
                return EmailValidationStatus.EmailAlreadyExist;

            return EmailValidationStatus.Done;
        }

        public static PasswordValidationStatus IsPasswordValid(string password)
        {
            if (password.Length > 16 || password.Length < 4)
                return PasswordValidationStatus.InvalidLength;

            if (password.Any(c => Char.IsDigit(c) && Char.IsUpper(c)))
                return PasswordValidationStatus.InvalidContent;

            return PasswordValidationStatus.Done;
        }


    }
}
