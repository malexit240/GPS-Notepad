using GPSNotepad.Core.Entities;

namespace GPSNotepad.Core
{
    public interface IRegistratorService
    {
        User Create(string login, string password);
    }
}
