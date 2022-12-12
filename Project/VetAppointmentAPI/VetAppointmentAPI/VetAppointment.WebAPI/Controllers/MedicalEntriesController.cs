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
        public async Task<IActionResult> Get()
        {
            return Ok(await medicalHistoryEntryRepository.All());
        }

        [HttpGet("{medicalEntryId:guid}")]
        public async Task<IActionResult> Get(Guid medicalEntryId)
        {
            MedicalHistoryEntry? medicalHistoryEntry = await medicalHistoryEntryRepository.Get(medicalEntryId);
            if (medicalHistoryEntry == null)
                return NotFound();

            MedicalEntryDetailDto medicalEntryDetailDto = new MedicalEntryDetailDto(medicalHistoryEntry);

            return Ok(medicalEntryDetailDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalEntryCreateDto medicalEntryDto)
        {
            var validation = medicalEntryValidator.Validate(medicalEntryDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            Appointment? appointment = await appointmentRepository.Get(medicalEntryDto.AppointmentId);
            if (appointment == null)
                return NotFound();

            Prescription? prescription = await prescriptionRepository.Get(medicalEntryDto.PrescriptionId);
            if (prescription == null)
                return NotFound();


            MedicalHistoryEntry medicalHistoryEntry = new MedicalHistoryEntry(appointment, prescription);
            await medicalHistoryEntryRepository.Add(medicalHistoryEntry);
            await medicalHistoryEntryRepository.SaveChanges();

            MedicalEntryDetailDto medicalEntryDetail = new MedicalEntryDetailDto(medicalHistoryEntry);
            return Created(nameof(Get), medicalEntryDetail);
        }

        [HttpDelete("{medicalEntryId:guid}")]
        public async Task<IActionResult> Delete(Guid medicalEntryId)
        {
            MedicalHistoryEntry? medicalHistoryEntry = await medicalHistoryEntryRepository.Get(medicalEntryId);
            if (medicalHistoryEntry == null)
                return NotFound();
            await medicalHistoryEntryRepository.Delete(medicalHistoryEntry);
            await medicalHistoryEntryRepository.SaveChanges();
            return NoContent();
        }
    }
}
