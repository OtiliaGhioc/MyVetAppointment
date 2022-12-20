using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Application.Commands;
using VetAppointment.Application.DTOs;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Helpers
{
    public class DrugMappingProfile : Profile
    {
        public DrugMappingProfile()
        {
            CreateMap<Drug, DrugResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.DrugId))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.Price, opt => opt.MapFrom(y => y.Price))
                .ReverseMap();

            CreateMap<Drug, CreateDrugCommand>().ReverseMap();
            CreateMap<Drug, UpdateDrugCommand>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.DrugId))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.Price, opt => opt.MapFrom(y => y.Price))
                .ReverseMap();
            CreateMap<Drug, DeleteDrugCommand>()
               .ReverseMap();

        }
    }
}
