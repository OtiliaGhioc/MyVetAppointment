using MediatR;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Queries
{
    public class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, List<OfficeResponse>>
    {
        private readonly IOfficeRepository repository;

        public GetAllOfficesQueryHandler(IOfficeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<OfficeResponse>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
        {
            var result = OfficeMapper.Mapper.Map<List<OfficeResponse>>(await repository.All());
            return result;
        }
    }
}
