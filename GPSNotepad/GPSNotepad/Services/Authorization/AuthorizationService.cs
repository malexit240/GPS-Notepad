using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using GPSNotepad.Repositories;
using GPSNotepad.Services.Authentication;
using GPSNotepad.Services.SecureStorageService;

namespace GPSNotepad.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {

        public AuthorizationService()
        {
            SecureStorageService = App.Current.Container.Resolve<ISecureStorageService>();
        }
        private static Guid _userId = Guid.Empty;
        protected ISecureStorageService SecureStorageService { get; set; }

        public bool IsAuthorized => SecureStorageService.SessionToken != Guid.Empty.ToString();


        #region ---IAuthorizatorService Implementation---


        public Guid GetCurrenUserId() => _userId;

        public async Task<bool> IsUserExist(string email)
        {
            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    return context.Users.Any((user) => user.Email == email);
                }
            });
        }

        public void SetAuthorize(Guid userId)
        {
            _userId = userId;
        }

        public void Unauthorize()
        {
            _userId = Guid.Empty;
            SecureStorageService.SessionToken = Guid.Empty.ToString();
        }
        #endregion
    }
}
