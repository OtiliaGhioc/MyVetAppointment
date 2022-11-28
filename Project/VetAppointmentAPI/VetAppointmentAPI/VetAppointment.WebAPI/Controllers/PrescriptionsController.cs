using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Impl;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository prescriptionRepository;
        public PrescriptionController(IPrescriptionRepository prescriptionRepository)
        {
            this.prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public IActionResult SearchPrescriptions([FromQuery] string? description)
        {
            var prescriptions = prescriptionRepository.Find(x => x.Description == description);
            if(prescriptions == null)
                return NotFound();
            return Ok(prescriptions);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllPrescriptions()
        {
            var prescriptions = prescriptionRepository.All().Select(p => new PrescriptionDto()
            {
                Id = p.PrescriptionId,
                Drugs = p.Drugs.ToList(),
                Description= p.Description

            });
            return Ok(prescriptions);
        }

        [HttpGet("{prescriptionId}")]
        public IActionResult GetById([FromRoute] Guid prescriptionId)
        {
            var prescription = prescriptionRepository.Get(prescriptionId);

            return prescription != null ? Ok(prescription) : NotFound($"Prescription with id: {prescriptionId} was not found");
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePrescriptionDto prescriptionDto)
        {
            var prescription = new Prescription(prescriptionDto.Description);
            prescriptionRepository.Add(prescription);
            prescriptionRepository.SaveChanges();
            return Created(nameof(GetAllPrescriptions), prescription);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CreatePrescriptionDto prescriptionDto)
        {
            var prescription = prescriptionRepository.Get(id);

            if (prescription == null)
            {
                NotFound($"Prescription with id: {id} was not found");
            }

            prescription.setDescription(prescriptionDto.Description);

            prescriptionRepository.Update(prescription);
            prescriptionRepository.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{prescriptionId}")]
        public IActionResult Delete([FromRoute] Guid prescriptionId)
        {
            var prescription = prescriptionRepository.Get(prescriptionId);

            if (prescription == null)
            {
                NotFound($"Prescription with id: {prescriptionId} was not found");
            }
            prescriptionRepository.Delete(prescription);
            prescriptionRepository.SaveChanges();
            return NoContent();
        }
    }
}
