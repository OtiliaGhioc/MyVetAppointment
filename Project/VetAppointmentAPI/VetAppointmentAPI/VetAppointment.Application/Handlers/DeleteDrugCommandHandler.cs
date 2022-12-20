using MediatR;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Helpers;
using VetAppointment.Application.Repositories.Interfaces;

namespace VetAppointment.Application.Handlers
{
    public class DeleteDrugCommandHandler : IRequestHandler<DeleteDrugCommand>
    {
        private readonly IDrugRepository repository;

        public DeleteDrugCommandHandler(IDrugRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteDrugCommand request, CancellationToken cancellationToken)
        {
            var drug = await repository.Get(request.Id);

            var drugEntity = DrugMapper.Mapper.Map(request,drug);
            if (drugEntity == null)
            {
                throw new Exception("Issue with the mapper");
            }
            await repository.DeleteAsync(drugEntity);
            return Unit.Value;
        }
    }
}
