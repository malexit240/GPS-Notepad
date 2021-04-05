using GPSNotepad.Model.Entities;
using System;
using System.Threading.Tasks;

namespace GPSNotepad.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> IsUserExist(string email);

        void SetAuthorize(Guid userId);

        bool IsAuthorized { get; }

        void Unauthorize();

        Guid GetCurrenUserId();
    }
}
