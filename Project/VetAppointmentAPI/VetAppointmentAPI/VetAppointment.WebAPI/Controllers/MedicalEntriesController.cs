using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MedicalEntriesController : ControllerBase
    {
        private readonly IMedicalHistoryEntryRepository medicalHistoryEntryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IValidator<MedicalEntryCreateDto> medicalEntryValidator;
        private readonly IMapper mapper;

        public MedicalEntriesController(IMedicalHistoryEntryRepository medicalHistoryEntryRepository,
            IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository, IValidator<MedicalEntryCreateDto> medicalEntryValidator, IMapper mapper)
        {
            this.medicalHistoryEntryRepository = medicalHistoryEntryRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.medicalEntryValidator = medicalEntryValidator;
            this.mapper = mapper;
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

            return Ok(mapper.Map<MedicalEntryDetailDto>(medicalHistoryEntry));
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


            MedicalHistoryEntry medicalHistoryEntry = mapper.Map<MedicalHistoryEntry>(medicalEntryDto);
            await medicalHistoryEntryRepository.Add(medicalHistoryEntry);
            await medicalHistoryEntryRepository.SaveChanges();

            return Created(nameof(Get), mapper.Map<MedicalEntryDetailDto>(medicalHistoryEntry));
        }

        [HttpDelete("{medicalEntryId:guid}")]
        public async Task<IActionResult> Delete(Guid medicalEntryId)
        {
            MedicalHistoryEntry? medicalHistoryEntry = await medicalHistoryEntryRepository.Get(medicalEntryId);
            if (medicalHistoryEntry == null)
                return NotFound();
            medicalHistoryEntryRepository.Delete(medicalHistoryEntry);
            await medicalHistoryEntryRepository.SaveChanges();
            return NoContent();
        }
    }
}
