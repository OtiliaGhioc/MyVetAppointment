﻿using MediatR;
using VetAppointment.Application.DTOs;

namespace VetAppointment.Application.Commands
{
    public class UpdateDrugCommand : IRequest<DrugResponse>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int? Price { get; set; }
    }
}