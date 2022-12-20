using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Handlers
{
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand>
    {
        private readonly IOfficeRepository repository;

        public DeleteOfficeCommandHandler(IOfficeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await repository.Get(request.Id);

            var officeEntity = OfficeMapper.Mapper.Map(request, office);
            if (officeEntity == null)
            {
                throw new Exception("Issue with the mapper");
            }
            await repository.DeleteAsync(officeEntity);
            return Unit.Value;
        }
    }
}
