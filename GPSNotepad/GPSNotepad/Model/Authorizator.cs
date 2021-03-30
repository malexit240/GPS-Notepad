using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GPSNotepad.Model
{
    public static class Authorizator
    {
        public static async Task<bool> AutorizeAsync(string email, string password)
        {
            CurrentUser.Instance = await App.Current.Container.Resolve<IAuthorizatorService>().Authorize(email, password);

            if (CurrentUser.Instance != null)
                App.Current.Container.Resolve<ISecureStorageService>().SessionToken = CurrentUser.Instance.SessionToken;

            return CurrentUser.Instance != null;
        }

        public static async Task<bool> ContinueSessionAsync()
        {
            CurrentUser.Instance = await App.Current.Container.Resolve<IAuthorizatorService>().ContinueSession(App.Current.Container.Resolve<ISecureStorageService>().SessionToken);
            return CurrentUser.Instance != null;
        }
    }
}
