using MediatR;

namespace VetAppointment.Application.Commands
{
    public class DeleteOfficeCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
