using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GPSNotepad.Database
{
    public class DBAuthorizatorService : IAuthorizatorService
    {
        public Task<User> Authorize(string login, string password)
        {
            using (var context = new Context())
            {
                var u = (from user in context.Users where user.Login == login && user.HashPassword == password select user).First();
                return u;
            }
        }

        public Task<bool> IsUserExist(string login)
        {
            throw new System.NotImplementedException();
        }
    }
}
