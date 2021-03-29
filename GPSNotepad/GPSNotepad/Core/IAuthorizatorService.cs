using GPSNotepad.Core.Entities;

namespace GPSNotepad.Core
{
    public interface IAuthorizatorService
    {
        bool IsUserExist(string login);

        User Authorize(string login, string password);

    }
}
