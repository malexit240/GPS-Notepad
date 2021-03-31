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
            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    var u = (from user in context.Users where user.Email == email && user.HashPassword == password select user).FirstOrDefault();
                    if (u != null)
                        CreateToken(ref u);
                    return u;
                }
            });
        }

        public User ContinueSession(string token)
        {

            User u = null;
            using (var context = new Context())
            {
                u = (from user in context.Users where user.SessionToken == token select user).FirstOrDefault();
            }
            return u;
        }

        public async Task<bool> IsUserExist(string email)
        {
            return await Task.Run(() =>
            {
                using (var context = new Context())
                {
                    return context.Users.Any((user) => user.Email == email);
                }
            });
        }

        private void CreateToken(ref User user)
        {
            user.SessionToken = Guid.NewGuid().ToString();

            using (var context = new Context())
            {
                context.Users.Update(user);
                context.SaveChanges();
            }
        }
    }
}
