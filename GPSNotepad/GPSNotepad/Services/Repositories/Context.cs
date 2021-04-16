using System;
using System.IO;
using GPSNotepad.Entities;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace GPSNotepad.Repositories
{
    public class Context : DbContext
    {
        #region ---Constructors---
        public Context()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }
        #endregion

        #region ---Public Properties---
        public DbSet<User> Users { get; set; }
        public DbSet<Pin> Pins { get; set; }
        public DbSet<PlaceEvent> Events { get; set; }
        #endregion

        #region ---Overrides---
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var dbPath = "db.sqlite";
            try
            {
                optionsBuilder.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, dbPath)}");
            }
            catch (Exception) // if tests run
            {
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
        }
        #endregion
    }
}
