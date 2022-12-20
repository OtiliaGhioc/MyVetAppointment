using MediatR;

namespace VetAppointment.Application.Commands
{
    public class DeleteDrugCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
