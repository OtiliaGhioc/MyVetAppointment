using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.Application.DTOs;
using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.WebAPI.DTOs;
using VetAppointment.Application.Queries;
using System.Collections.Generic;
using VetAppointment.Application.Helpers;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository drugRepository;
        private readonly IMediator mediator;

        public DrugsController(IDrugRepository drugRepository,IMediator mediator)
        {
            this.drugRepository = drugRepository;
            this.mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<List<DrugResponse>> GetAllDrugs()
        {
            return await mediator.Send(new GetAllDrugsQuery());
        }

        [HttpGet("{drugId}")]
        public async Task<ActionResult<DrugResponse>> GetById([FromRoute] Guid drugId)
        {
            return await mediator.Send(new GetDrugByIdQuery
            {
                Id = drugId
            });
        }

        [HttpPost]
        public async Task<ActionResult<DrugResponse>> Create([FromBody] CreateDrugCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDrugCommand command)
        {
            await mediator.Send(command);
            return NoContent();
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
