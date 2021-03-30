using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System;
using System.Threading.Tasks;

namespace GPSNotepad.Database
{
    public class DBRegistratorService : IRegistratorService
    {
        public async Task<bool> Registrate(string email, string login, string password)
        {
            return await Task.Factory.StartNew(() =>
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    Login = login,
                    HashPassword = password,
                    Pins = new System.Collections.Generic.List<Pin>()
                };

                using (var context = new Context())
                {
                    context.Users.Add(user);
                    context.SaveChangesAsync();
                    return true;
                }

            });
        }
    }
}
