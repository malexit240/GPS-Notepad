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
            if (password.Length > 16 || password.Length < 4)
                return PasswordValidationStatus.InvalidLength;

            if (password.Any(c => Char.IsDigit(c) && Char.IsUpper(c)))
                return PasswordValidationStatus.InvalidContent;

            return PasswordValidationStatus.Done;
        }
        #endregion
    }
}
