using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;
using VetAppointment.WebAPI.DTOs;
using VetAppointment.WebAPI.Validators;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingEntryController : ControllerBase
    {
        private readonly IBillingEntryRepository billingEntryRepository;
        private readonly IUserRepository issuerRepository;
        private readonly IUserRepository customerRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IValidator<BillingEntryDto> billEntryValidator;

        public BillingEntryController(IBillingEntryRepository billingEntryRepository, IUserRepository userRepository, IUserRepository officeRepository, IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository, IValidator<BillingEntryDto> validator)
        {
            this.billingEntryRepository = billingEntryRepository;
            this.issuerRepository = userRepository;
            this.customerRepository = officeRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.billEntryValidator = validator;
        }


        [HttpGet("{billingEntryId}")]
        public IActionResult SearchBillingById([FromRoute] Guid billingEntryId)
        {
            var bill = billingEntryRepository.Get(billingEntryId);

            return bill != null ? Ok(bill) : NotFound($"The Bill with id: {billingEntryId} was not found");
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllBills()
        {
            var drugStocks = billingEntryRepository.All().Select(bill => new BillingEntryDto()
            {
                BillingEntryId = bill.BillingEntryId,
                IssuerId = bill.IssuerId,
                CustomerId = bill.CustomerId,
                DateTime = bill.DateTime,
                AppointmentId = bill.AppointmentId,
                PrescriptionId = bill.PrescriptionId,
                Price = bill.Price
            });
            return Ok(drugStocks);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BillingEntryDto billDto)
        {
            var validation = billEntryValidator.Validate(billDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);

            var customer = customerRepository.Get(billDto.CustomerId);
            if (customer == null)
                return NotFound($"The Customer with id: {billDto.CustomerId} was not found");

            var issuer = issuerRepository.Get(billDto.IssuerId);
            if (issuer == null)
                return NotFound($"The Issuer with id: {billDto.IssuerId} was not found");

            var appointment = appointmentRepository.Get(billDto.AppointmentId);
            if (appointment == null)
                return NotFound($"The Appoinment with id: {billDto.AppointmentId} was not found");

            var prescription = prescriptionRepository.Get(billDto.PrescriptionId);
            if (prescription == null)
                return NotFound($"The Prescription with id: {billDto.PrescriptionId} was not found");

            var bill = new BillingEntry(issuer, customer, billDto.DateTime, prescription, appointment, billDto.Price);
            billingEntryRepository.Add(bill);
            billingEntryRepository.SaveChanges();
            return Created(nameof(GetAllBills), bill);

        }

        [HttpDelete("{billingEntryId}")]
        public IActionResult Delete([FromRoute] Guid billingEntryId)
        {
            var bill = billingEntryRepository.Get(billingEntryId);

            if (bill == null)
            {
                NotFound($"The Bill with id: {billingEntryId} was not found");
            }
            billingEntryRepository.Delete(bill);
            billingEntryRepository.SaveChanges();
            return NoContent();
        }

    }
}
