using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.DTOs
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public List<PrescriptionDrug>? Drugs { get; set; }
        public string? Description { get; set; }

        public Prescription ApplyModificationsToModel(Prescription prescription)
        {
            if (Description != null)
                prescription.Description = Description;
            if (Drugs != null)
                foreach (var drug in Drugs)
                {
                    prescription.AddDrugToPrescription(drug);
                }
            return prescription;
        }
    }
}
