using MediatR;
using VetAppointment.Application.Dtos;

namespace VetAppointment.Application.Queries
{
    public class GetOfficeByIdQuery : IRequest<OfficeResponse>
    {
        public Guid Id { get; set; }
    }
}
