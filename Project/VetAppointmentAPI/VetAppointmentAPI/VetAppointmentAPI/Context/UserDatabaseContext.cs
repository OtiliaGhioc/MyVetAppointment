using Microsoft.EntityFrameworkCore;
using VetAppointmentAPI.Entities;

namespace VetAppointmentAPI.Context
{
    public class UserDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }

        public UserDatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Main.db");
        }
    }
}
