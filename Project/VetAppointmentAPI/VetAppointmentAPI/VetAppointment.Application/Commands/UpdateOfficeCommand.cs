using MediatR;
using VetAppointment.Application.Dtos;

namespace VetAppointment.Application.Commands
{
    public class UpdateOfficeCommand : IRequest<OfficeResponse>
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
    }
}
