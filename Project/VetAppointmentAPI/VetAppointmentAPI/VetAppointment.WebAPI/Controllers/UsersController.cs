﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IValidator<DefaultUserDto> userValidator;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepository, IAppointmentRepository appointmentRepository, IValidator<DefaultUserDto> validator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.appointmentRepository = appointmentRepository;
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
            var meData = await GetParsedUserData(user);
            return meData;
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

            List<Appointment> userAppointments =
                (await appointmentRepository.Find(item => !item.IsExpired && item.AppointeeId == user.UserId)).ToList();

            List<User> appointers = new List<User>();
            foreach (Appointment appointment in userAppointments)
            {
                User? appointer = await userRepository.Get(appointment.AppointerId);
                if (appointer == null)
                    userAppointments.Remove(appointment);
                else
                    appointers.Add(appointer);
            }
            CompleteUserDto userDto = new CompleteUserDto(user, userAppointments, appointers);
            return Ok(userDto);
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
