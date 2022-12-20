using MediatR;
using VetAppointment.Application.Dtos;

namespace VetAppointment.Application.Queries
{
    public class GetAllOfficesQuery : IRequest<List<OfficeResponse>>
    {
    }
}
