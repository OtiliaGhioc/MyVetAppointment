using MediatR;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Queries;
using VetAppointment.Application.Repositories.Interfaces;

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
        [HttpPost]
        public async Task<ActionResult<OfficeResponse>> Post([FromBody] CreateOfficeCommand command)
        {
            var res =  await mediator.Send(command);
            return res == null ? BadRequest() : Created(nameof(Get), res);
        }

        // PUT api/<OfficesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateOfficeCommand command)
        {
            var res = await mediator.Send(command);
            return res == null ? BadRequest() : Ok(res);
        }

        // DELETE api/<OfficesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await mediator.Send(new DeleteOfficeCommand
            {
                Id = id
            });
            return NoContent();
        }
    }
}
