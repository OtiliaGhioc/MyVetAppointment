using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.UserDto;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMedicalHistoryEntryRepository medicalHistoryEntryRepository;
        private readonly IValidator<DefaultUserDto> userValidator;
        private readonly IMapper mapper;

        public UsersController(
            IUserRepository userRepository, 
            IAppointmentRepository appointmentRepository, 
            IMedicalHistoryEntryRepository medicalHistoryEntryRepository, 
            IValidator<DefaultUserDto> validator, 
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.appointmentRepository = appointmentRepository;
            this.medicalHistoryEntryRepository = medicalHistoryEntryRepository;
            this.userValidator= validator;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();

            var meData = new DetailUserDto(user);
            return Ok(meData);
        }

        [Authorize]
        [HttpGet("me/appointments")]
        public async Task<IActionResult> GetMyAppointments()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();

            List<Appointment> userAppointments = (List<Appointment>)await GetUserAppointments(user);
            var userAppointers = await GetAppointersForUserAppointments(userAppointments);
            List<User> appointers = (List<User>)userAppointers.Item1;
            userAppointments = (List<Appointment>)userAppointers.Item2;

            return Ok(new UserAppointmentsDto(user, userAppointments, appointers));
        }

        [Authorize]
        [HttpGet("me/medical-history")]
        public async Task<IActionResult> GetMyMedicalHistory()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();

            List<MedicalHistoryEntry> userMedicalHistoryEntries = (List<MedicalHistoryEntry>)await GetUserMedicalHistoryEntries(user);
            var userAppointers = await GetAppointersForUserMedicalHistoryEntries(userMedicalHistoryEntries);
            List<User> appointers = (List<User>)userAppointers.Item1;
            userMedicalHistoryEntries = (List<MedicalHistoryEntry>)userAppointers.Item2;

            return Ok(new UserMedicalHistoryDto(user, userMedicalHistoryEntries, appointers));
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await userRepository.All());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            User? user = await userRepository.Get(id);
            var userData = await GetParsedUserData(user);
            return userData;
        }

        private async Task<IActionResult> GetParsedUserData(User? user)
        {
            if (user == null)
                return NotFound();

            List<Appointment> userAppointments = (List<Appointment>)await GetUserAppointments(user);
            var userAppointers = await GetAppointersForUserAppointments(userAppointments);
            List<User> appointers = (List<User>)userAppointers.Item1;
            userAppointments = (List<Appointment>)userAppointers.Item2;

            CompleteUserDto userDto = new CompleteUserDto(user, userAppointments, appointers);
            return Ok(userDto);
        }

        private async Task<IEnumerable<Appointment>> GetUserAppointments(User user)
        {
            List<Appointment> userAppointments =
                (await appointmentRepository.Find(item => !item.IsExpired && item.AppointeeId == user.UserId)).ToList();
            return userAppointments;
        }

        private async Task<IEnumerable<MedicalHistoryEntry>> GetUserMedicalHistoryEntries(User user)
        {
            List<MedicalHistoryEntry> userMedicalEntries = new();
            return userMedicalEntries;
        }

        private async Task<Tuple<IEnumerable<User>, IEnumerable<Appointment>>> 
            GetAppointersForUserAppointments(List<Appointment> userAppointments)
        {
            List<User> appointers = new List<User>();
            foreach (Appointment appointment in userAppointments)
            {
                User? appointer = await userRepository.Get(appointment.AppointerId);
                if (appointer == null)
                    userAppointments.Remove(appointment);
                else
                    appointers.Add(appointer);
            }
            return new Tuple<IEnumerable<User>, IEnumerable<Appointment>>(appointers, userAppointments);
        }

        private async Task<Tuple<IEnumerable<User>, IEnumerable<MedicalHistoryEntry>>>
            GetAppointersForUserMedicalHistoryEntries(List<MedicalHistoryEntry> userMedicalHistoryEntries)
        {
            List<User> appointers = new List<User>();
            foreach (MedicalHistoryEntry medicalHistoryEntry in userMedicalHistoryEntries)
            {
                var appointerId = medicalHistoryEntry.Appointment?.AppointerId;

                if (appointerId == null)
                {
                    userMedicalHistoryEntries.Remove(medicalHistoryEntry);
                    continue;
                }

                User? appointer = await userRepository.Get((Guid)appointerId);

                if (appointer == null)
                    userMedicalHistoryEntries.Remove(medicalHistoryEntry);
                else
                    appointers.Add(appointer);
            }
            return new Tuple<IEnumerable<User>, IEnumerable<MedicalHistoryEntry>>(appointers, userMedicalHistoryEntries);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DefaultUserDto userDto)
        {
            var validation = userValidator.Validate(userDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            User? user = await userRepository.Get(id);
            if (user == null)
                return NotFound();

            userRepository.Update(user);
            await userRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            User? user = await userRepository.Get(id);
            if(user == null)
                return NotFound();
            userRepository.Delete(user);
            await userRepository.SaveChanges();

            return NoContent();
        }
    }
}
