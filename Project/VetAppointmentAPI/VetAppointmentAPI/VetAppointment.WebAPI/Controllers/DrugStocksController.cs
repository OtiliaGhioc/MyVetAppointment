﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.WebAPI.Validators;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugStocksController : ControllerBase
    {
        private readonly IDrugStockRepository drugStockRepository;
        private readonly IDrugRepository drugRepository;
        private readonly IValidator<CreateDrugStockDto> drugValidator;

        public DrugStocksController(IDrugStockRepository drugStockRepository, IDrugRepository drugRepository, IValidator<CreateDrugStockDto> validator)
        {
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
            this.drugValidator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchDrugStockss([FromQuery] Guid? typeId, [FromQuery] int? quantity)
        {
            var drugs = await drugStockRepository.Find(x => x.TypeId == typeId || x.Quantity == quantity);
            if( drugs != null)
                return Ok(drugs);
            return NotFound();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDrugStocks()
        {
            var drugStocks = await drugStockRepository.All();
            return Ok(drugStocks);
        }

        [HttpGet("{drugStockId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid drugStockId)
        {
            var drug = await drugStockRepository.Get(drugStockId);

            return drug != null ? Ok(drug) : NotFound($"DrugStock with id: {drugStockId} was not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrugStockDto drugDto)
        {
            var validation = drugValidator.Validate(drugDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);

            var drug = await drugRepository.Get(drugDto.TypeId);

            if (drug == null)
                return NotFound($"Drug with id: {drugDto.TypeId} was not found");

            var drugStock = new DrugStock(drug,drugDto.Quantity);
            await drugStockRepository.Add(drugStock);
            await drugStockRepository.SaveChanges();
            return Created(nameof(GetAllDrugStocks), drugStock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] int quantityUpdate)
        {
            var drug = await drugStockRepository.Get(id);

            if (drug == null)
            {
                return NotFound($"Drug with id: {id} was not found");
            }

            drug.RemoveDrugsFromPublicStock(quantityUpdate);

            await drugStockRepository.Update(drug);
            await drugStockRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{drugStockId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drugStockId)
        {
            var drug = await drugStockRepository.Get(drugStockId);

            if (drug == null)
            {
                return NotFound($"DrugStock with id: {drugStockId} was not found");
            }
            await drugStockRepository.Delete(drug);
            await drugStockRepository.SaveChanges();
            return NoContent();
        }

    }
}
