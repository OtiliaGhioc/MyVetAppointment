using Microsoft.AspNetCore.Mvc;
using System;
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

        public DrugStocksController(IDrugStockRepository drugStockRepository, IDrugRepository drugRepository)
        {
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
        }

        [HttpGet]
        public IActionResult SearchDrugStockss([FromQuery] Guid? typeId, [FromQuery] int? quantity)
        {
            return (typeId != null && quantity != null) ? Ok(drugStockRepository.Find(x => x.TypeId == typeId && x.Quantity == quantity)) : Ok(drugStockRepository.Find(x => x.TypeId == typeId || x.Quantity == quantity));
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
            var drug = drugRepository.Get(drugDto.TypeId);

            if (drug == null)
                return NotFound($"Drug with id: {drugDto.TypeId} was not found");

            var drugStock = new DrugStock(drug,drugDto.Quantity);
            drugStockRepository.Add(drugStock);
            drugStockRepository.SaveChanges();
            return Created(nameof(GetAllDrugStocks), drugStock);
        }

        /*[HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CreateDrugDto drugDto)
        {
            var drug = drugRepository.Get(id);

            if(drug == null)
            {
                NotFound($"Drug with id: {id} was not found");
            }

            drug.Price=drugDto.Price;
            drug.Title=drugDto.Title;
            
            drugRepository.Update(drug);
            drugRepository.SaveChanges();
            return NoContent();
        }*/

        [HttpDelete("{drugStockId}")]
        public IActionResult Delete([FromRoute] Guid drugStockId)
        {
            var drug = drugStockRepository.Get(drugStockId);

            if (drug == null)
            {
                NotFound($"DrugStock with id: {drugStockId} was not found");
            }
            drugStockRepository.Delete(drug);
            drugStockRepository.SaveChanges();
            return NoContent();
        }

    }
}
