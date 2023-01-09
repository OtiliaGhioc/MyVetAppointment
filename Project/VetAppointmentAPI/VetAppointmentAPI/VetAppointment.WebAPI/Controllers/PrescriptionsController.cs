using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetAppointment.Application.DTOs;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public PrescriptionsController(IPrescriptionRepository prescriptionRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.prescriptionRepository = prescriptionRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var prescriptions = await prescriptionRepository.All();
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();

            List<MedicalHistoryEntry> userMedicalHistoryEntries = (List<MedicalHistoryEntry>)await GetUserMedicalHistoryEntries(user);

            var filteredPrescriptions = new List<Prescription>();
            foreach (var presc in prescriptions)
            {
                foreach (var medHistEnt in userMedicalHistoryEntries)
                {
                    if (presc.PrescriptionId == medHistEnt.PrescriptionId)
                    {
                        filteredPrescriptions.Add(presc);
                        break;
                    }
                }
            }

            //return Ok(filteredPrescriptions);
            //PRESCRIPTIILE SUNT LEGATE DE USER PRIN MEDICAL HISTORY;
            //PANA NU E IMPLEMENTAT ALA CA SA PUTEM ADAUGA PRESCRIPTIA IN MEDICAL HISTORY DEGEABA FILTRAM

            return Ok(prescriptions);
        }

        private async Task<IEnumerable<MedicalHistoryEntry>> GetUserMedicalHistoryEntries(User user)
        {
            List<MedicalHistoryEntry> userMedicalEntries = new();
            return userMedicalEntries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Prescription? prescription = await prescriptionRepository.Get(id);
            if (prescription == null)
                return NotFound();

            return Ok(new PrescriptionDto());
        }

        //[Authorize(Policy = "medic")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionDto prescriptionDto)
        {
            await prescriptionRepository.Add(new Prescription(prescriptionDto.Description));
            return Ok();
        }

        //[Authorize(Policy = "medic")]
        [HttpPut("{prescriptionId:guid}")]
        public async Task<IActionResult> Update(Guid prescriptionId, [FromBody] PrescriptionDto prescriptionDto)
        {
            Prescription? prescription = await prescriptionRepository.Get(prescriptionId);
            if (prescription == null)
                return NotFound();
            prescription = prescriptionDto.ApplyModificationsToModel(prescription);
            mapper.Map(prescriptionDto, prescription);
            prescriptionRepository.Update(prescription);
            await prescriptionRepository.SaveChanges();
            return NoContent();
        }

        //[Authorize(Policy = "medic")]
        [HttpDelete("{prescriptionId:guid}")]
        public async Task<IActionResult> Delete(Guid prescriptionId)
        {
            Prescription? prescription = await prescriptionRepository.Get(prescriptionId);
            if (prescription == null)
                return NotFound();
            prescriptionRepository.Delete(prescription);
            await prescriptionRepository.SaveChanges();
            return NoContent();
        }
    }
}
