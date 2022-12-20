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
            var result = await mediator.Send(new GetDrugByIdQuery
            {
                Id = drugId
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DrugResponse>> Create([FromBody] CreateDrugCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDrugCommand command)
        {
            var drug = await mediator.Send(new GetDrugByIdQuery
            {
                Id = id
            });

            if (drug == null)
            {
                return NotFound($"Drug with id: {id} was not found");
            }

            var result = await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{drugId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drugId)
        {
            var drug = await mediator.Send(new GetDrugByIdQuery
            {
                Id = drugId
            });

            if (drug == null)
            {
                return NotFound($"Drug with id: {drugId} was not found");
            }
            var result = await mediator.Send(new DeleteDrugCommand());
            return NoContent();
        }
    }
}
