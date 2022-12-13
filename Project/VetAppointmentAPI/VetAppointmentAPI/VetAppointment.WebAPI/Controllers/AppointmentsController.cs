using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IValidator<AppointmentCreateDto> appoinmentValidator;
        public AppointmentsController(IAppointmentRepository appointmentRepository, IUserRepository userRepository, IValidator<AppointmentCreateDto> validator)
        {
            this.appointmentRepository = appointmentRepository;
            this.userRepository = userRepository;
            this.appoinmentValidator= validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await appointmentRepository.All());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Appointment? appointment = await appointmentRepository.Get(id);
            if (appointment == null) 
                return NotFound();

            User? appointer = await userRepository.Get(appointment.AppointerId);
            User? appointee = await userRepository.Get(appointment.AppointeeId);

            if (appointer == null || appointee == null)
                return NotFound();

            return Ok(new AppontmentEssentialExtendedDto(appointment, appointer, appointee));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto appointmentDto)
        {
            var validation = appoinmentValidator.Validate(appointmentDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            User? appointee = await userRepository.Get(appointmentDto.AppointeeId);
            if (appointee == null)
                return NotFound();

            User? appointer = await userRepository.Get(appointmentDto.AppointerId);
            if (appointer == null)
                return NotFound();


            Appointment appointment = new Appointment(appointer, appointee, appointmentDto.DueDate, appointmentDto.Title, 
                appointmentDto.Description, appointmentDto.Type);
            await appointmentRepository.Add(appointment);
            await appointmentRepository.SaveChanges();

            AppointmentDetailDto appointmentDetail = new AppointmentDetailDto(appointment);
            return Created(nameof(Get), appointmentDetail);
        }

        [HttpPut("{appointmentId:guid}")]
        public async Task<IActionResult> Update(Guid appointmentId, [FromBody] AppointmentModifyDto appointmentDto)
        {
            Appointment? appointment = await appointmentRepository.Get(appointmentId);
            if (appointment == null)
                return NotFound();
            appointment = appointmentDto.ApplyModificationsToModel(appointment);
            await appointmentRepository.Update(appointment);
            await appointmentRepository.SaveChanges();
            return Ok(new AppointmentDetailDto(appointment));
        }

        [HttpDelete("{appointmentId:guid}")]
        public async Task<IActionResult> Delete(Guid appointmentId)
        {
            Appointment? appointment = await appointmentRepository.Get(appointmentId);
            if (appointment == null)
                return NotFound();
            await appointmentRepository.Delete(appointment);
            await appointmentRepository.SaveChanges();
            return NoContent();
        }
    }
}
