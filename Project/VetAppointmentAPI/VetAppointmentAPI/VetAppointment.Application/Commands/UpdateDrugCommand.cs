using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Commands
{
    public class UpdateDrugCommand : IRequest<DrugResponse>
    {
        public string? Title { get; set; }
        public int? Price { get; set; }
    }
}
