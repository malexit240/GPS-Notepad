using GPSNotepad.Model.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GPSNotepad.Model
{
    public static class Authorizator
    {
        #region ---Public Static Methods---
        public static async Task<bool> AutorizeAsync(string email, string password)
        {
            CurrentUser.Instance = await App.Current.Container.Resolve<IAuthorizatorService>().Authorize(email, password);

            if (CurrentUser.Instance != null)
                App.Current.Container.Resolve<ISecureStorageService>().SessionToken = CurrentUser.Instance.SessionToken;

            return CurrentUser.Instance != null;
        }

        public static bool ContinueSession()
        {
            var token = App.Current.Container.Resolve<ISecureStorageService>().SessionToken;

            CurrentUser.Instance = App.Current.Container.Resolve<IAuthorizatorService>().ContinueSession(token);

            return CurrentUser.Instance != null;
        } 
        #endregion
    }
}
