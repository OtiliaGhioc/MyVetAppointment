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

        public OfficesController(IOfficeRepository officeRepository)
        {
            this.officeRepository = officeRepository;
        }

        // GET: api/<OfficesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(officeRepository.All());
        }

        // GET api/<OfficesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var office = officeRepository.Get(id);
            if(office == null)
            {
                return NotFound();
            }
            return Ok(office);
        }

        // POST api/<OfficesController>
        [HttpPost]
        public IActionResult Post([FromBody] OfficeDto officeDto)
        {
            Office office = new Office(officeDto.Address);
            officeRepository.Add(office);
            officeRepository.SaveChanges();

            return Created(nameof(Office), office);
        }

        // PUT api/<OfficesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] OfficeDto officeDto)
        {
            Office? office = officeRepository.Get(officeDto.OfficeId);
            if (office == null)
                return NotFound();

            officeRepository.Update(office);
            officeRepository.SaveChanges();

            return Ok(office);
        }

        // DELETE api/<OfficesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Office office = officeRepository.Get(id);
            officeRepository.Delete(office);
            officeRepository.SaveChanges();

            return NoContent();
        }
    }
}
