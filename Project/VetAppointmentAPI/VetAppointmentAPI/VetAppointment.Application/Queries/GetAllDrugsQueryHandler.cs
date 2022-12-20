using MediatR;
using VetAppointment.Application.DTOs;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Queries
{
    public class GetAllDrugsQueryHandler : IRequestHandler<GetAllDrugsQuery, List<DrugResponse>>
    {
        private readonly IDrugRepository repository;

        public GetAllDrugsQueryHandler(IDrugRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<DrugResponse>> Handle(GetAllDrugsQuery request, CancellationToken cancellationToken)
        {
            var result = DrugMapper.Mapper.Map<List<DrugResponse>>(await repository.All());
            return result;
        }
    }
}
