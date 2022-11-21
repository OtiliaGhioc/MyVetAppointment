using Microsoft.EntityFrameworkCore;
using VetAppointmentAPI.Entities;

namespace VetAppointmentAPI.Context
{
    public class MedicalEntryContext: DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalHistoryEntry> MedicalEntries { get; set; }

        public MedicalEntryContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Main.db");
        }
    }
}
