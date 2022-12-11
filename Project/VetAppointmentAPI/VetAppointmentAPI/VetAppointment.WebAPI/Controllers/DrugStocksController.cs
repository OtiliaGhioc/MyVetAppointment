using FluentValidation;
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
        public IActionResult SearchDrugStockss([FromQuery] Guid? typeId, [FromQuery] int? quantity)
        {
            var drugs = drugStockRepository.Find(x => x.TypeId == typeId || x.Quantity == quantity);
            if( drugs != null)
                return Ok(drugs);
            return NotFound();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllDrugStocks()
        {
            var drugStocks = drugStockRepository.All().Select(d => new DrugStockDto()
            {
                Id = d.DrugStockId,
                Type = d.Type,
                TypeId = d.TypeId,
                Quantity = d.Quantity,
            });
            return Ok(drugStocks);
        }

        [HttpGet("{drugStockId}")]
        public IActionResult GetById([FromRoute] Guid drugStockId)
        {
            var drug = drugStockRepository.Get(drugStockId);

            return drug != null ? Ok(drug) : NotFound($"DrugStock with id: {drugStockId} was not found");
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDrugStockDto drugDto)
        {
            var validation = drugValidator.Validate(drugDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);

            var drug = drugRepository.Get(drugDto.TypeId);

            if (drug == null)
                return NotFound($"Drug with id: {drugDto.TypeId} was not found");

            var drugStock = new DrugStock(drug,drugDto.Quantity);
            drugStockRepository.Add(drugStock);
            drugStockRepository.SaveChanges();
            return Created(nameof(GetAllDrugStocks), drugStock);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] int quantityUpdate)
        {
            var drug = drugStockRepository.Get(id);

            if (drug == null)
            {
                return NotFound($"Drug with id: {id} was not found");
            }

            drug.RemoveDrugsFromPublicStock(quantityUpdate);

            drugStockRepository.Update(drug);
            drugStockRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{drugStockId}")]
        public IActionResult Delete([FromRoute] Guid drugStockId)
        {
            var drug = drugStockRepository.Get(drugStockId);

            if (drug == null)
            {
                return NotFound($"DrugStock with id: {drugStockId} was not found");
            }
            drugStockRepository.Delete(drug);
            drugStockRepository.SaveChanges();
            return NoContent();
        }

    }
}
