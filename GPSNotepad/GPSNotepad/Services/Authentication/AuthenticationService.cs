using GPSNotepad.Entities;
using System;
using System.Threading.Tasks;
using GPSNotepad.Repositories;
using System.Linq;
using GPSNotepad.Services.Authorization;
using GPSNotepad.Services.Settings;

namespace GPSNotepad.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        #region ---Constructors---
        public AuthenticationService()
        {
            AuthorizationService = App.Current.Container.Resolve<IAuthorizationService>();
            SettingsManager = App.Current.Container.Resolve<ISettingsManagerService>();
        }
        #endregion

        #region ---Protected Properties---
        protected IAuthorizationService AuthorizationService { get; set; }
        protected ISettingsManagerService SettingsManager { get; set; }
        #endregion


        #region ---IAuthenticationService Implementation---
        public bool ContinueSession(string token)
        {
            bool result = false;

            User user;
            using var context = new Context();

            user = (from u in context.Users where u.SessionToken == token select u).FirstOrDefault();

            if (user != null)
            {
                AuthorizationService.SetAuthorize(user.UserId);
                result = true;
            }

            return result;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            return await Task.Run(() =>
            {
                bool result = false;

                using var context = new Context();

                var u = (from user in context.Users where user.Email == email && user.HashPassword == password select user).FirstOrDefault();

                if (u != null)
                {
                    CreateToken(ref u);

                    AuthorizationService.SetAuthorize(u.UserId);
                    SettingsManager.SessionToken = u.SessionToken;

                    result = true;
                }

                return result;
            });
        }

        public async Task<bool> SignUpAsync(string email, string login, string password)
        {
            return await Task.Run(() =>
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    Login = login,
                    HashPassword = password,
                    Pins = new System.Collections.Generic.List<Pin>()
                };

                using var context = new Context();

                context.Users.Add(user);
                context.SaveChangesAsync();

                return true;
            });
        }

        private void CreateToken(ref User user)
        {
            user.SessionToken = TokenGenerator.Get();

            using var context = new Context();

            context.Users.Update(user);
            context.SaveChanges();
        }
        #endregion
    }
}
