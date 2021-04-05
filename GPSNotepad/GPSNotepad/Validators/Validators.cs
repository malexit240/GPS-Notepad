using System;
using System.Threading.Tasks;
using GPSNotepad.Model.Interfaces;
using System.Linq;
using GPSNotepad.Services.Authorization;

namespace GPSNotepad.Validators
{
    public static class Validators
    {
        #region ---Public Static Methods---
        public static async Task<EmailValidationStatus> IsEmailValid(string email)
        {

            try
            {
                var adress = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                return EmailValidationStatus.InvalidFormat;
            }

            if (await App.Current.Container.Resolve<IAuthorizationService>().IsUserExist(email))
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
        #endregion


    }
}
