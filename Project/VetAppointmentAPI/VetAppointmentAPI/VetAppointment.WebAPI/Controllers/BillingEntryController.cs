using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingEntryController : ControllerBase
    {
        private readonly IBillingEntryRepository billingEntryRepository;
        private readonly IUserRepository userRepository;
        private readonly IOfficeRepository officeRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPrescriptionRepository prescriptionRepository;

        public BillingEntryController(IBillingEntryRepository billingEntryRepository, IUserRepository userRepository, IOfficeRepository officeRepository, IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository)
        {
            this.billingEntryRepository = billingEntryRepository;
            this.userRepository = userRepository;
            this.officeRepository = officeRepository;
            this.appointmentRepository = appointmentRepository;
            this.prescriptionRepository = prescriptionRepository;
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
            var bill = billingEntryRepository.Get(billDto.BillingEntryId);

            if (bill == null)
                return NotFound($"The Bill with id: {billDto.BillingEntryId} was not found");

            var Bill = new BillingEntry(bill.Issuer, bill.Customer, bill.DateTime, bill.Prescription, bill.Appointment, bill.Price);
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
