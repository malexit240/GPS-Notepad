using System.Threading.Tasks;

namespace GPSNotepad.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> SignInAsync(string email, string password);

        bool ContinueSession(string token);

        Task<bool> SignUpAsync(string email, string login, string password);
    }
}
