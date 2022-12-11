using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;
using VetAppointment.WebAPI.Validators;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalEntriesController : ControllerBase
    {
        private readonly IMedicalHistoryEntryRepository medicalHistoryEntryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IValidator<MedicalEntryCreateDto> medicalEntryValidator;

        public MedicalEntriesController(IMedicalHistoryEntryRepository medicalHistoryEntryRepository,
            IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository, IValidator<MedicalEntryCreateDto> medicalEntryValidator)
        {
            this.medicalHistoryEntryRepository = medicalHistoryEntryRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.medicalEntryValidator = medicalEntryValidator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicalHistoryEntryRepository.All().ToList().Select(item => new MedicalEntryDetailDto(item)));
        }

        [HttpGet("{medicalEntryId:guid}")]
        public IActionResult Get(Guid medicalEntryId)
        {
            MedicalHistoryEntry? medicalHistoryEntry = medicalHistoryEntryRepository.Get(medicalEntryId);
            if (medicalHistoryEntry == null)
                return NotFound();

            MedicalEntryDetailDto medicalEntryDetailDto = new MedicalEntryDetailDto(medicalHistoryEntry);

            return Ok(medicalEntryDetailDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MedicalEntryCreateDto medicalEntryDto)
        {
            var validation = medicalEntryValidator.Validate(medicalEntryDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
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
