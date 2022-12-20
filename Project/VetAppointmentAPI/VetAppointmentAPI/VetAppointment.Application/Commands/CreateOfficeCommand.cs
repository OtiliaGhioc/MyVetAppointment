using MediatR;
using VetAppointment.Application.Dtos;

namespace VetAppointment.Application.Commands
{
    public class CreateOfficeCommand : IRequest<OfficeResponse>
    {
        public string? Address { get; set; }
    }
}
