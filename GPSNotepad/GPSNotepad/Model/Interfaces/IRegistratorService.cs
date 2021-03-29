using GPSNotepad.Model.Entities;

namespace GPSNotepad.Model.Interfaces
{
    public interface IRegistratorService
    {
        User Registrate(string login, string password);
    }
}
