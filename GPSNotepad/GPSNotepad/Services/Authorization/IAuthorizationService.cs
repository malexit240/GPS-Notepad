using System;
using System.Threading.Tasks;

namespace GPSNotepad.Services.Authorization
{
    public interface IAuthorizationService
    {
        Guid GetCurrenUserId();

        Task<bool> IsUserExist(string email);

        void SetAuthorize(Guid userId);

        bool IsAuthorized { get; }

        void LogOut();

    }
}
