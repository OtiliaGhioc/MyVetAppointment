using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.DTOs;
using VetAppointment.WebAPI.Util;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DrugStocksController : ControllerBase
    {
        private readonly IDrugStockRepository drugStockRepository;
        private readonly IDrugRepository drugRepository;
        private readonly IUserRepository userRepository;
        private readonly IValidator<CreateDrugStockDto> drugValidator;
        private readonly IMapper mapper;

        public DrugStocksController(
            IDrugStockRepository drugStockRepository,
            IDrugRepository drugRepository,
            IUserRepository userRepository,
            IValidator<CreateDrugStockDto> validator, IMapper mapper)
        {
            this.drugStockRepository = drugStockRepository;
            this.drugRepository = drugRepository;
            this.userRepository = userRepository;
            this.drugValidator = validator;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? typeId, [FromQuery] int? quantity)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Forbid();
            User? user = await userRepository.Get(Guid.Parse(userId));
            if (user == null)
                return Forbid();

            if (!user.IsMedic)
                return Forbid();

            List<Func<DrugStock, bool>> searchQueries = new();
            if (typeId != null)
                searchQueries.Add(x => x.TypeId == typeId);
            if (quantity != null)
                searchQueries.Add(x => x.Quantity == quantity);
            
            var drugStocks = await drugStockRepository.Find(FilteringUtil.CreateAndExpression(searchQueries));

            if (drugStocks == null)
                return BadRequest();

            List<DrugStockDetailDto> drugStocksDtos = new();
            foreach(var drugStock in drugStocks)
            {
                var drug = await drugRepository.Get(drugStock.TypeId);
                if (drug != null)
                    drugStocksDtos.Add(new DrugStockDetailDto(drugStock, drug));
            }

            return Ok(drugStocksDtos);
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
            return Created(nameof(Get), mapper.Map<DrugStockDto>(drugStock));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDrugStockDto drugDto)
        {
            var drug = await drugStockRepository.Get(id);

            if (drug == null)
            {
                return NotFound($"Drug with id: {id} was not found");
            }

            if(drugDto.SupplyQuantity != null)
                drug.AddDrugsInStock((int)drugDto.SupplyQuantity);
            if (drugDto.PricePerItem != null)
                drug.UpdatePricePerItem((int)drugDto.PricePerItem);

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
