using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
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
        private readonly IMapper mapper;

        public DrugStocksController(IDrugStockRepository drugStockRepository, IDrugRepository drugRepository, IValidator<CreateDrugStockDto> validator, IMapper mapper)
        {
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
            this.drugValidator = validator;
            this.mapper = mapper;
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

            return drug != null ? Ok(mapper.Map<DrugStockDto>(drug)) : NotFound($"DrugStock with id: {drugStockId} was not found");
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

            DrugStock drugStock = mapper.Map<DrugStock>(drugDto);
            await drugStockRepository.Add(drugStock);
            await drugStockRepository.SaveChanges();
            return Created(nameof(GetAllDrugStocks), mapper.Map<DrugStockDto>(drugStock));
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

            drugStockRepository.Update(drug);
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
            drugStockRepository.Delete(drug);
            await drugStockRepository.SaveChanges();
            return NoContent();
        }

    }
}
