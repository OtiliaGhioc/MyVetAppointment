using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Application.Commands
{
    public class DeleteOfficeCommand:IRequest
    {
        public Guid Id { get; set; }
    }
}
