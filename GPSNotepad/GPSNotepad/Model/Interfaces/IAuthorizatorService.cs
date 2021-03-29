using GPSNotepad.Model.Entities;

namespace GPSNotepad.Model.Interfaces
{
    public interface IAuthorizatorService
    {
        bool IsUserExist(string login);

        User Authorize(string login, string password);

    }
}
