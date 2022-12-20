using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Queries;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeRepository officeRepository;
        private readonly IMediator mediator;

        public OfficesController(IOfficeRepository officeRepository, IMediator mediator)
        {
            this.officeRepository = officeRepository;
            this.mediator = mediator;
        }

        // GET: api/<OfficesController>
        [HttpGet]
        public async Task<List<OfficeResponse>> Get()
        {
            return await mediator.Send(new GetAllOfficesQuery());
        }

        // GET api/<OfficesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeResponse>> Get(Guid id)
        {
            return await mediator.Send(new GetOfficeByIdQuery
            {
                Id = id
            });
        }

        // POST api/<OfficesController>
        [HttpPost]
        public async Task<ActionResult<OfficeResponse>> Post([FromBody] CreateOfficeCommand command)
        {
            return await mediator.Send(command);
        }

        // PUT api/<OfficesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateOfficeCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<OfficesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteOfficeCommand
            {
                Id = id
            });
            return NoContent();
        }
    }
}
