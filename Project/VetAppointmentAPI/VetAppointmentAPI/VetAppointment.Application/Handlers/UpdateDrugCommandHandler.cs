using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.DTOs;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Handlers
{
    public class UpdateDrugCommandHandler : IRequestHandler<UpdateDrugCommand, DrugResponse>
    {
        private readonly IDrugRepository repository;

        public UpdateDrugCommandHandler(IDrugRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DrugResponse> Handle(UpdateDrugCommand request, CancellationToken cancellationToken)
        {
            var drugEntity = DrugMapper.Mapper.Map<Drug>(request);
            if (drugEntity == null)
            {
                throw new ApplicationException("Issue with the mapper");
            }
            var newDrug = await repository.UpdateAsync(drugEntity);
            return DrugMapper.Mapper.Map<DrugResponse>(newDrug);
        }
    }
}
