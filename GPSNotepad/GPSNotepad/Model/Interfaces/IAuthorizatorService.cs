using GPSNotepad.Model.Entities;
using System.Threading.Tasks;

namespace GPSNotepad.Model.Interfaces
{
    public interface IAuthorizatorService
    {
        Task<bool> IsUserExist(string email);

        Task<User> Authorize(string email, string password);

        Task<User> ContinueSession(string token);

    }
}
