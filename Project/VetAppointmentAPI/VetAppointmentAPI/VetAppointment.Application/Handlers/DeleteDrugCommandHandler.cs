using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Application.Commands;
using VetAppointment.Application.DTOs;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Handlers
{
    public class DeleteDrugCommandHandler : IRequestHandler<DeleteDrugCommand, DrugResponse>
    {
        private readonly IDrugRepository repository;

        public DeleteDrugCommandHandler(IDrugRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DrugResponse> Handle(DeleteDrugCommand request, CancellationToken cancellationToken)
        {
            var drugEntity = DrugMapper.Mapper.Map<Drug>(request);
            if (drugEntity == null)
            {
                throw new ApplicationException("Issue with the mapper");
            }
            var newDrug = await repository.DeleteAsync(drugEntity);
            return DrugMapper.Mapper.Map<DrugResponse>(newDrug);
        }
    }
}
