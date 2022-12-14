using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BillingEntryController : ControllerBase
    {
        private readonly IBillingEntryRepository billingEntryRepository;
        private readonly IUserRepository issuerRepository;
        private readonly IUserRepository customerRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IValidator<BillingEntryDto> billEntryValidator;
        private readonly IMapper mapper;

        public BillingEntryController(IBillingEntryRepository billingEntryRepository, IUserRepository userRepository, IUserRepository officeRepository, IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository, IValidator<BillingEntryDto> validator, IMapper mapper)
        {
            this.billingEntryRepository = billingEntryRepository;
            this.issuerRepository = userRepository;
            this.customerRepository = officeRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.billEntryValidator = validator;
            this.mapper = mapper;
        }


        [HttpGet("{billingEntryId}")]
        public async Task<IActionResult> SearchBillingById([FromRoute] Guid billingEntryId)
        {
            var bill = await billingEntryRepository.Get(billingEntryId);

            return bill != null ? Ok(mapper.Map<BillingEntryDto>(bill)) : NotFound($"The Bill with id: {billingEntryId} was not found");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBills()
        {
            var drugStocks = await billingEntryRepository.All();
            return Ok(drugStocks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillingEntryDto billDto)
        {
            var validation = billEntryValidator.Validate(billDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);

            var customer = await customerRepository.Get(billDto.CustomerId);
            if (customer == null)
                return NotFound($"The Customer with id: {billDto.CustomerId} was not found");

            var issuer = await issuerRepository.Get(billDto.IssuerId);
            if (issuer == null)
                return NotFound($"The Issuer with id: {billDto.IssuerId} was not found");

            var appointment = await appointmentRepository.Get(billDto.AppointmentId);
            if (appointment == null)
                return NotFound($"The Appoinment with id: {billDto.AppointmentId} was not found");

            var prescription = await prescriptionRepository.Get(billDto.PrescriptionId);
            if (prescription == null)
                return NotFound($"The Prescription with id: {billDto.PrescriptionId} was not found");

            BillingEntry bill = mapper.Map<BillingEntry>(billDto);
            await billingEntryRepository.Add(bill);
            await billingEntryRepository.SaveChanges();
            return Created(nameof(GetAllBills), mapper.Map<BillingEntryDto>(bill));

        }

        [HttpDelete("{billingEntryId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid billingEntryId)
        {
            var bill = await billingEntryRepository.Get(billingEntryId);

            if (bill == null)
            {
                return NotFound($"The Bill with id: {billingEntryId} was not found");
            }
            billingEntryRepository.Delete(bill);
            await billingEntryRepository.SaveChanges();
            return NoContent();
        }

    }
}
