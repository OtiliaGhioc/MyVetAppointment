using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Handlers
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, OfficeResponse>
    {
        private readonly IOfficeRepository repository;

        public CreateOfficeCommandHandler(IOfficeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OfficeResponse> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var officeEntity = OfficeMapper.Mapper.Map<Office>(request);
            if (officeEntity == null)
            {
                throw new ApplicationException("Issue with the mapper");
            }
            var newOffice = await repository.Add(officeEntity);
            return OfficeMapper.Mapper.Map<OfficeResponse>(newOffice);
        }
    }
}
