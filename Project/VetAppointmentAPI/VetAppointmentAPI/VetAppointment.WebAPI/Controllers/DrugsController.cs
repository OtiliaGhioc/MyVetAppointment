using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Application.DTOs;
using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Queries;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DrugsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<DrugResponse>> GetAllDrugs()
        {
            return await mediator.Send(new GetAllDrugsQuery());
        }

        [HttpGet("{drugId}")]
        public async Task<ActionResult<DrugResponse>> GetById([FromRoute] Guid drugId)
        {
            var res = await mediator.Send(new GetDrugByIdQuery
            {
                Id = drugId
            });

            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<DrugResponse>> Create([FromBody] CreateDrugCommand command)
        {
            var res = await mediator.Send(command);
            return res == null ? BadRequest() : Created(nameof(GetAllDrugs), res);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDrugCommand command)
        {
            var res = await mediator.Send(command);
            return res == null ? BadRequest() : Ok(res);
        }

        [HttpDelete("{drugId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drugId)
        {
            await mediator.Send(new DeleteDrugCommand
            {
                Id = drugId
            });
            return NoContent();
        }
    }
}
