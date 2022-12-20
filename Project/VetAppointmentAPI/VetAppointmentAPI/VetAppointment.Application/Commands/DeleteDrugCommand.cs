using MediatR;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Commands
{
    public class DeleteDrugCommand:IRequest
    {
        public Guid Id { get; set; }
    }
}
