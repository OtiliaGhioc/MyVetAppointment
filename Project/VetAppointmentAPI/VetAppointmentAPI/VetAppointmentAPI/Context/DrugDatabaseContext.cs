using Microsoft.EntityFrameworkCore;
using VetAppointmentAPI.Entities;

namespace VetAppointmentAPI.Context
{
    public class DrugDatabaseContext : DbContext
    {
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugStock> DrugStocks { get; set; }

        public DrugDatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Main.db");
        }
    }
}
