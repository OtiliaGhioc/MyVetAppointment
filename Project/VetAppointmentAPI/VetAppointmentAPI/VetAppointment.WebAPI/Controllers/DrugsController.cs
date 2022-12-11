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
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository drugRepository;
        private readonly IValidator<CreateDrugDto> drugValidator;
        public DrugsController(IDrugRepository drugRepository, IValidator<CreateDrugDto> validator)
        {
            this.drugRepository = drugRepository;
            this.drugValidator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchDrugs([FromQuery] string? title, [FromQuery] int? price)
        {
            var drugStocks = await drugRepository.Find(x => x.Title == title || x.Price == price);
            if (drugStocks != null)
                return Ok(drugStocks);
            return NotFound();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDrugs()
        {
            var drugs = await drugRepository.All();
            return drugs != null ? Ok(drugs) : NotFound();
        }

        [HttpGet("{drugId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid drugId)
        {
            var drug = await drugRepository.Get(drugId);

            return drug != null ? Ok(drug) : NotFound($"Drug with id: {drugId} was not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrugDto drugDto)
        {
            var validation = drugValidator.Validate(drugDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);
            var drug = new Drug(drugDto.Title, drugDto.Price);
            await drugRepository.Add(drug);
            await drugRepository.SaveChanges();
            return Created(nameof(GetAllDrugs), drug);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateDrugDto drugDto)
        {
            var validation = drugValidator.Validate(drugDto);
            if (!validation.IsValid)
                return StatusCode(400, validation.Errors.First().ErrorMessage);

            var drug = await drugRepository.Get(id);

            if (drug == null)
            {
                return NotFound($"Drug with id: {id} was not found");
            }

            drug.UpdateNameAndPrice(drugDto.Title, drugDto.Price);

            await drugRepository.Update(drug);
            await drugRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{drugId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid drugId)
        {
            var drug = await drugRepository.Get(drugId);

            if (drug == null)
            {
                return NotFound($"Drug with id: {drugId} was not found");
            }
            await drugRepository.Delete(drug);
            await drugRepository.SaveChanges();
            return NoContent();
        }
    }
}
