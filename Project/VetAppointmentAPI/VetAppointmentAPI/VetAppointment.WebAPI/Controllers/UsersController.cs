using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userRepository.All());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(userRepository.Get(id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] string name, [FromBody] string password, [FromBody] bool hasOffice)
        {
            User user = new User(name, password, hasOffice);
            userRepository.Add(user);
            userRepository.SaveChanges();

            return Created(nameof(User), user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            User user = userRepository.Get(id);
            userRepository.Delete(user);
            userRepository.SaveChanges();

            return NoContent();
        }
    }
}
