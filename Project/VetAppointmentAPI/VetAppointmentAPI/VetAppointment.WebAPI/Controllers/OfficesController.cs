using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;

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
            return Ok(officeRepository.Get(id));
        }

        // POST api/<OfficesController>
        [HttpPost]
        public IActionResult Post([FromBody] string address)
        {
            Office office = new Office(address);
            officeRepository.Add(office);
            officeRepository.SaveChanges();

            return Created(nameof(Office), office);
        }

        // PUT api/<OfficesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
