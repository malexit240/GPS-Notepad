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
        public async Task<User> Authorize(string email, string password)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    var u = (from user in context.Users where user.Email == email && user.HashPassword == password select user).FirstOrDefault();
                    return u;
                }
            });
        }

        public async Task<bool> IsUserExist(string email)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    return context.Users.Any((user) => user.Email == email);
                }
            });
        }
    }
}
