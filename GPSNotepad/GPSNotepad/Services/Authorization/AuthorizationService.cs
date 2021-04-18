using System;
using System.Linq;
using System.Threading.Tasks;
using GPSNotepad.Repositories;
using GPSNotepad.Services.SecureStorageService;

namespace GPSNotepad.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        #region ---Private Static Fields---
        private static Guid _userId = Guid.Empty;
        #endregion

        #region ---Constructors---
        public AuthorizationService()
        {
            this.SecureStorageService = App.Current.Container.Resolve<ISecureStorageService>();
        }
        #endregion

        #region ---Protected Properties---
        protected ISecureStorageService SecureStorageService { get; set; }
        #endregion

        #region ---Public Properties---
        public bool IsAuthorized => SecureStorageService.SessionToken != Guid.Empty.ToString();
        #endregion

        #region ---IAuthorizatorService Implementation---
        public Guid GetCurrenUserId() => _userId;

        public async Task<bool> IsUserExist(string email)
        {
            return await Task.Run(() =>
            {
                using var context = new Context();
                return context.Users.Any((user) => user.Email == email);
            });
        }

        public void SetAuthorize(Guid userId) => _userId = userId;

        public void LogOut()
        {
            _userId = Guid.Empty;
            SecureStorageService.SessionToken = Guid.Empty.ToString();
        }
        #endregion
    }
}
