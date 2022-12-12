using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeRepository officeRepository;
        private readonly IValidator<OfficeDto> officeValidator;

        public OfficesController(IOfficeRepository officeRepository, IValidator<OfficeDto> validator)
        {
            this.officeRepository = officeRepository;
            this.officeValidator = validator;
        }

        // GET: api/<OfficesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await officeRepository.All());
        }

        // GET api/<OfficesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var office = await officeRepository.Get(id);
            if(office == null)
            {
                return NotFound();
            }
            return Ok(office);
        }

        // POST api/<OfficesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OfficeDto officeDto)
        {
            var validation = officeValidator.Validate(officeDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            Office office = new Office(officeDto.Address);
            await officeRepository.Add(office);
            await officeRepository.SaveChanges();

            return Created(nameof(Office), office);
        }

        // PUT api/<OfficesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OfficeDto officeDto)
        {
            var validation = officeValidator.Validate(officeDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            Office? office = await officeRepository.Get(officeDto.OfficeId);
            if (office == null)
                return NotFound();

            await officeRepository.Update(office);
            await officeRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/<OfficesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var office = await officeRepository.Get(id);
            if (office == null)
                return NotFound();
            await officeRepository.Delete(office);
            await officeRepository.SaveChanges();

            return NoContent();
        }
    }
}
