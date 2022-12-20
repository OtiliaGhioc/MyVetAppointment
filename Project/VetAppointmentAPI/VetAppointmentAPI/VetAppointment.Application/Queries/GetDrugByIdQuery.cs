using MediatR;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Queries
{
    public class GetDrugByIdQuery : IRequest<DrugResponse>
    {
        public Guid Id { get; set; }
    }
}
