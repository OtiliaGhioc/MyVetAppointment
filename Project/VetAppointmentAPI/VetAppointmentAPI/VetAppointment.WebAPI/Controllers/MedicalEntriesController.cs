using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Impl;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalEntriesController : ControllerBase
    {
        private readonly MedicalHistoryEntryRepository medicalHistoryEntryRepository;
        private readonly AppointmentRepository appointmentRepository;
        private readonly PrescriptionRepository prescriptionRepository;

        public MedicalEntriesController(MedicalHistoryEntryRepository medicalHistoryEntryRepository,
            AppointmentRepository appointmentRepository, PrescriptionRepository prescriptionRepository)
        {
            this.medicalHistoryEntryRepository = medicalHistoryEntryRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalHistoryEntryRepository.All().ToList().Select(item => new MedicalEntryDetailDto(item)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] MedicalEntryCreateDto medicalEntryDto)
        {
            Appointment? appointment = appointmentRepository.Get(medicalEntryDto.AppointmentId);
            if (appointment == null)
                return NotFound();

            Prescription? prescription = prescriptionRepository.Get(medicalEntryDto.PrescriptionId);
            if (prescription == null)
                return NotFound();


            MedicalHistoryEntry medicalHistoryEntry = new MedicalHistoryEntry(appointment, prescription);
            medicalHistoryEntryRepository.Add(medicalHistoryEntry);
            medicalHistoryEntryRepository.SaveChanges();

            MedicalEntryDetailDto medicalEntryDetail = new MedicalEntryDetailDto(medicalHistoryEntry);
            return Created(nameof(Get), medicalEntryDetail);
        }

        [HttpDelete("{medicalEntryId:guid}")]
        public IActionResult Delete(Guid medicalEntryId)
        {
            MedicalHistoryEntry? medicalHistoryEntry = medicalHistoryEntryRepository.Get(medicalEntryId);
            if (medicalHistoryEntry == null)
                return NotFound();
            medicalHistoryEntryRepository.Delete(medicalHistoryEntry);
            medicalHistoryEntryRepository.SaveChanges();
            return NoContent();
        }
    }
}
