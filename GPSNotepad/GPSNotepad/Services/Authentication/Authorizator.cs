using System.Threading.Tasks;

namespace GPSNotepad.Model
{
    public static class Authorizator
    {
        #region ---Public Static Methods---
        public static async Task<bool> AutorizeAsync(string email, string password)
        {
            return false;
            //CurrentUser.Instance = await App.Current.Container.Resolve<IAuthorizationService>().Authorize/(email,/ password);
            //
            //if (CurrentUser.Instance != null)
            //    App.Current.Container.Resolve<ISecureStorageService>().SessionToken = //CurrentUser.Instance.SessionToken;
            //
            //return CurrentUser.Instance != null;
        }

        public static bool ContinueSession()
        {

            return false;
            //var token = App.Current.Container.Resolve<ISecureStorageService>().SessionToken;
            //
            //CurrentUser.Instance = App.Current.Container.Resolve<IAuthorizationService>().ContinueSession//(token);
            //
            //return CurrentUser.Instance != null;
        }
        #endregion
    }
}
