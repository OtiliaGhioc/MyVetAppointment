﻿using VetAppointment.Domain.Helpers;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Domain.Entities
{
    public class Prescription
    {
        public Prescription(string description)
        {
            PrescriptionId = Guid.NewGuid();
            Drugs = new List<PrescriptionDrug>();
            Description = description;
        }

        public Guid PrescriptionId { get; private set; }
        public IEnumerable<PrescriptionDrug> Drugs { get; private set; } = new List<PrescriptionDrug>();
        public string Description { get; private set; }
        
        public Result AddDrugToPrescription(PrescriptionDrug drug)
        {
            if (drug == null)
                return Result.Failure("Cannot add a null value to prescription");
            Drugs.Append(drug);
            return Result.Success();
        }
    }
}
