using System;
using System.Linq;
using System.Text;
using GPSNotepad.Enums;

using System.Text.RegularExpressions;

namespace GPSNotepad.Validators
{
    public static class Validators
    {
        #region ---Public Static Methods---
        public static EmailValidationStatus IsEmailValid(string email)
        {
            var status = Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") ? EmailValidationStatus.Done : EmailValidationStatus.InvalidFormat;

            return status;
        }

        public static PasswordValidationStatus IsPasswordValid(string password)
        {
            PasswordValidationStatus status;

            if (password.Length > 16 || password.Length < 4)
            {
                status = PasswordValidationStatus.InvalidLength;
            }
            else if (!password.Any(c => Char.IsDigit(c)) || !password.Any(c => Char.IsUpper(c)))
            {
                status = PasswordValidationStatus.InvalidContent;
            }
            else
            {
                status = PasswordValidationStatus.Done;
            }


            return status;
        }
        #endregion
    }
}
