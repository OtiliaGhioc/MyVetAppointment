using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Queries;
using VetAppointment.Application.Repositories.Impl;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos;
using VetAppointment.WebAPI.Validators;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IOfficeRepository officeRepository;
        private readonly IValidator<OfficeDto> validator;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public OfficesController(IMediator mediator, IOfficeRepository officeRepository,IValidator<OfficeDto> validator, IUserRepository userRepository, IMapper mapper)
        {
            this.mediator = mediator;
            this.officeRepository = officeRepository;
            this.validator = validator;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        // GET: api/<OfficesController>
        [HttpGet]
        public async Task<ActionResult<List<OfficeResponse>>> Get()
        {
            var res = await mediator.Send(new GetAllOfficesQuery());
            return res == null ? BadRequest() : Ok(res);
        }

        // GET api/<OfficesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeResponse>> Get(Guid id)
        {
            var res = await mediator.Send(new GetOfficeByIdQuery
            {
                Id = id
            });

            return res == null ? NotFound() : Ok(res);
        }

        // POST api/<OfficesController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OfficeResponse>> Post([FromBody] OfficeDto officeDto)
        {
            var validation = validator.Validate(officeDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            Office office = new Office(officeDto.Address);
            await officeRepository.Add(office);
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();
            user.RegisterOfficeToUser(office);
            await officeRepository.SaveChanges();

            return Created(nameof(Office), office);
        }

        // PUT api/<OfficesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]Guid id,[FromBody] OfficeDto officeDto)
        {
            var validation = validator.Validate(officeDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            Office? office = await officeRepository.Get(id);
            if (office == null)
                return NotFound();

            mapper.Map(officeDto, office);

            officeRepository.Update(office);
            await officeRepository.SaveChanges();

            return NoContent();
        }

        // DELETE api/<OfficesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var office = await officeRepository.Get(id);
            if (office == null)
                return NotFound();
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return NotFound();
            user.UnregisterOfficeFromUser();
            await userRepository.SaveChanges();

            officeRepository.Delete(office);
            await officeRepository.SaveChanges();
            return NoContent();
        }
    }
}
