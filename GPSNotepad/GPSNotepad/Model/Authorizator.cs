using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.Model
{
    public static class Authorizator
    {
        public static void Autorize(string login, string password)
        {
            CurrentUser.Instance = App.Current.Container.Resolve<IAuthorizatorService>().Authorize(login, password);
        }
    }
}
