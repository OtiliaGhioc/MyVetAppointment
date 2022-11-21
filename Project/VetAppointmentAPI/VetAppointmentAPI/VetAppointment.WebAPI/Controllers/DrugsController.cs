using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository drugRepository;
        public DrugsController(IDrugRepository drugRepository)
        {
            this.drugRepository = drugRepository;
        }

        [HttpGet]
        public IActionResult SearchDrugs([FromQuery] string? title, [FromQuery] int? price)
        {
            return (title != null && price != null) ? Ok(drugRepository.Find(x => x.Title == title && x.Price == price)) : Ok(drugRepository.Find(x => x.Title == title || x.Price == price));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllDrugs()
        {
            var drugs = drugRepository.All().Select(d => new DrugDto()
            {
                Id = d.DrugId,
                Title = d.Title,
                Price = d.Price
            });
            return Ok(drugs);
        }

        [HttpGet("{drugId}")]
        public IActionResult GetById([FromRoute] Guid drugId)
        {
            var drug = drugRepository.Get(drugId);

            return drug != null ? Ok(drug) : NotFound($"Drug with id: {drugId} was not found");
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDrugDto drugDto)
        {
            var drug = new Drug(drugDto.Title, drugDto.Price);
            drugRepository.Add(drug);
            drugRepository.SaveChanges();
            return Created(nameof(GetAllDrugs), drug);
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

        [HttpDelete("{drugId}")]
        public IActionResult Delete([FromRoute] Guid drugId)
        {
            var drug = drugRepository.Get(drugId);

            if (drug == null)
            {
                NotFound($"Drug with id: {drugId} was not found");
            }
            drugRepository.Delete(drug);
            drugRepository.SaveChanges();
            return NoContent();
        }
    }
}
