using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.UserDto;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public UsersController(IUserRepository userRepository, IAppointmentRepository appointmentRepository)
        {
            this.userRepository = userRepository;
            this.appointmentRepository = appointmentRepository;
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
            User? user = userRepository.Get(id);
            if (user == null)
                return NotFound();

            List<Appointment> userAppointments = 
                appointmentRepository.Find(item => !item.IsExpired && item.AppointeeId == user.UserId).ToList();

            List<User> appointers = new List<User>();
            foreach(Appointment appointment in userAppointments)
            {
                User? appointer = userRepository.Get(appointment.AppointerId);
                if(appointer == null)
                    userAppointments.Remove(appointment);
                else
                    appointers.Add(appointer);
            }
            CompleteUserDto userDto = new CompleteUserDto(user, userAppointments, appointers);
            return Ok(userDto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] DefaultUserDto userDto)
        {
            User user = new User(userDto.Username, userDto.Password);
            userRepository.Add(user);
            userRepository.SaveChanges();

            return Created(nameof(User), user);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult Put([FromBody] DefaultUserDto userDto)
        {
            User? user = userRepository.Get(userDto.UserId);
            if (user == null)
                return NotFound();

            userRepository.Update(user);
            userRepository.SaveChanges();

            return Ok(user);
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
