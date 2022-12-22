using MediatR;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Commands
{
    public class CreateDrugCommand : IRequest<DrugResponse>
    {
        public string? Title { get; set; }
    }
}
