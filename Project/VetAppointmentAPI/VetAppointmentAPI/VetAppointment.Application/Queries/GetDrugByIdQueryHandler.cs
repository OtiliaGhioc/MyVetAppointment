using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Application.DTOs;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Queries
{
    public class GetDrugByIdQueryHandler : IRequestHandler<GetDrugByIdQuery, DrugResponse>
    {
        private readonly IDrugRepository repository;

        public GetDrugByIdQueryHandler(IDrugRepository repository)
        {
            this.repository = repository;
        }
        public async Task<DrugResponse> Handle(GetDrugByIdQuery request, CancellationToken cancellationToken)
        {
            var response = DrugMapper.Mapper.Map<DrugResponse>(await repository.Get(request.Id));
            return response;
        }
    }
}
