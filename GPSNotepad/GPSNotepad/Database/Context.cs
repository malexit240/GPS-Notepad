using System.IO;
using GPSNotepad.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace GPSNotepad.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pin> Pins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            try
            {
                optionsBuilder.UseSqlite(Path.Combine(FileSystem.AppDataDirectory, "db.sqlite"));
            }
            catch (Xamarin.Essentials.NotImplementedInReferenceAssemblyException) // if tests run
            {
                optionsBuilder.UseSqlite("db.sqlite");
            }
        }
    }
}
