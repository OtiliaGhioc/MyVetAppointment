using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Handlers
{
    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, OfficeResponse>
    {
        private readonly IOfficeRepository repository;

        public UpdateOfficeCommandHandler(IOfficeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OfficeResponse> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            var officeEntity = await repository.Get(request.Id);
            var office = OfficeMapper.Mapper.Map(request, officeEntity);
            if (office == null)
            {
                throw new ApplicationException("Issue with the mapper");
            }
            var newOffice = await repository.UpdateAsync(office);
            return OfficeMapper.Mapper.Map<OfficeResponse>(newOffice);
        }
    }
}
