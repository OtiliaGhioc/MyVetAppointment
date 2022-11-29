using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IUserRepository userRepository;
        public AppointmentsController(IAppointmentRepository appointmentRepository, IUserRepository userRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(appointmentRepository.All().ToList().Select(item => new AppointmentDetailDto(item)).ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentCreateDto appointmentDto)
        {
            User? appointee = userRepository.Get(appointmentDto.AppointeeId);
            if (appointee == null)
                return NotFound();

            User? appointer = userRepository.Get(appointmentDto.AppointerId);
            if (appointer == null)
                return NotFound();


            Appointment appointment = new Appointment(appointer, appointee, DateTime.Now, appointmentDto.Title, 
                appointmentDto.Description, appointmentDto.Type);
            appointmentRepository.Add(appointment);
            appointmentRepository.SaveChanges();

            AppointmentDetailDto appointmentDetail = new AppointmentDetailDto(appointment);
            return Created(nameof(Get), appointmentDetail);
        }

        [HttpPut("{appointmentId:guid}")]
        public IActionResult Update(Guid appointmentId, [FromBody] AppointmentModifyDto appointmentDto)
        {
            Appointment? appointment = appointmentRepository.Get(appointmentId);
            if (appointment == null)
                return NotFound();
            appointment = appointmentDto.ApplyModificationsToModel(appointment);
            appointmentRepository.Update(appointment);
            appointmentRepository.SaveChanges();
            return Ok(new AppointmentDetailDto(appointment));
        }

        [HttpDelete("{appointmentId:guid}")]
        public IActionResult Delete(Guid appointmentId)
        {
            Appointment? appointment = appointmentRepository.Get(appointmentId);
            if (appointment == null)
                return NotFound();
            appointmentRepository.Delete(appointment);
            appointmentRepository.SaveChanges();
            return NoContent();
        }
    }
}
