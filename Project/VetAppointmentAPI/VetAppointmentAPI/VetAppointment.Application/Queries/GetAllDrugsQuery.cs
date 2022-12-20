using MediatR;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Queries
{
    public class GetAllDrugsQuery : IRequest<List<DrugResponse>>
    {
    }
}
