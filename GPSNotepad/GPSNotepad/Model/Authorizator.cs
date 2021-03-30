using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.Model
{
    public static class Authorizator
    {
        public static async void AutorizeAsync(string email, string password)
        {
            CurrentUser.Instance = await App.Current.Container.Resolve<IAuthorizatorService>().Authorize(email, password);
        }
    }
}
