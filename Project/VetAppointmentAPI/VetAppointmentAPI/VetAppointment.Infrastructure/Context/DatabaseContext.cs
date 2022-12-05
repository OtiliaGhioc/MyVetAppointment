using Microsoft.EntityFrameworkCore;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Infrastructure.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        //public DatabaseContext()
        //{
        //    this.Database.EnsureCreated();
        //}
        public DbSet<User> Users => Set<User>();
        public DbSet<Office> Offices => Set<Office>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<MedicalHistoryEntry> MedicalEntries => Set<MedicalHistoryEntry>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<PrescriptionDrug> PrescriptionDrugs => Set<PrescriptionDrug>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<DrugStock> DrugStocks => Set<DrugStock>();
        public DbSet<BillingEntry> BillingEntries => Set<BillingEntry>();

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = VetAppointmentMainDB.db");
        }
    }
}
