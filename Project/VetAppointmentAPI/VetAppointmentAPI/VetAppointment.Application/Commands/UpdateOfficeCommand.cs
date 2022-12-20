using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Application.Dtos;

namespace VetAppointment.Application.Commands
{
    public class UpdateOfficeCommand : IRequest<OfficeResponse>
    {
        public string? Address { get; set; }
    }
}
