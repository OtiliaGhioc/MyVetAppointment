using Microsoft.EntityFrameworkCore;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Infrastructure.Context
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; }
        DbSet<Office> Offices { get; }
        DbSet<Appointment> Appointments { get; }
        DbSet<MedicalHistoryEntry> MedicalEntries { get; }
        DbSet<Prescription> Prescriptions { get; }
        DbSet<PrescriptionDrug> PrescriptionDrugs { get; }
        DbSet<Drug> Drugs { get; }
        DbSet<DrugStock> DrugStocks { get; }

        void Save();
    }
}
