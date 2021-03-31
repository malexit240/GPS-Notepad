using System;
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

        public Context()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

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
    }
}
