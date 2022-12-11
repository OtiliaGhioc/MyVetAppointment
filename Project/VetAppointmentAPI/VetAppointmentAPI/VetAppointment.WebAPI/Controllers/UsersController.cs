using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.Validators;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IValidator<DefaultUserDto> userValidator;

        public UsersController(IUserRepository userRepository, IAppointmentRepository appointmentRepository, IValidator<DefaultUserDto> validator)
        {
            this.userRepository = userRepository;
            this.appointmentRepository = appointmentRepository;
            this.userValidator= validator;
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
            var validation = userValidator.Validate(userDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            User user = new User(userDto.Username, userDto.Password);
            userRepository.Add(user);
            userRepository.SaveChanges();

            return Created(nameof(User), user);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult Put([FromBody] DefaultUserDto userDto)
        {
            var validation = userValidator.Validate(userDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            User? user = userRepository.Get(userDto.UserId);
            if (user == null)
                return NotFound();

            userRepository.Update(user);
            userRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            User user = userRepository.Get(id);
            if(user == null)
                return NotFound();
            userRepository.Delete(user);
            userRepository.SaveChanges();

            return NoContent();
        }
    }
}
