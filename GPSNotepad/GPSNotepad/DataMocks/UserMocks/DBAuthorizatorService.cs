using GPSNotepad.Core;
using GPSNotepad.Core.Entities;
using GPSNotepad.DatabaseMocks;

namespace GPSNotepad.DatabaseMocks.UserMocks
{
    public class DBAuthorizatorService : IAuthorizatorService
    {
        public User Authorize(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUserExist(string login)
        {
            throw new System.NotImplementedException();
        }
    }
}
