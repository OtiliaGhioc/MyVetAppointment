using MediatR;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Queries
{
    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, OfficeResponse>
    {
        private readonly IOfficeRepository repository;

        public GetOfficeByIdQueryHandler(IOfficeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<OfficeResponse> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var response = OfficeMapper.Mapper.Map<OfficeResponse>(await repository.Get(request.Id));
            return response;
        }
    }
}
