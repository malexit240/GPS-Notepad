using GPSNotepad.Model.Entities;
using System.Threading.Tasks;

namespace GPSNotepad.Model.Interfaces
{
    public interface IRegistratorService
    {
        Task<bool> Registrate(string email,string login, string password);
    }
}
