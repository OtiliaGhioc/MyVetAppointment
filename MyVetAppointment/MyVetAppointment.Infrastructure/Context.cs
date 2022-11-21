using Microsoft.EntityFrameworkCore;
using MyVetAppointment.Domain.Entities;

namespace MyVetAppointment.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugStock> DrugStocks { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<BilingEntry> BilingEntries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = VetAppointment.db");
        }
    }
}
