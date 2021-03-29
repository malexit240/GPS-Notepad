using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System;

namespace GPSNotepad.Database
{
    public class DBRegistratorService : IRegistratorService
    {
        public User Registrate(string login, string password)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Login = login,
                HashPassword = password
            };

            using (var context = new Context())
            {
                context.Users.Add(user);
            }

            return user;
        }
    }
}
