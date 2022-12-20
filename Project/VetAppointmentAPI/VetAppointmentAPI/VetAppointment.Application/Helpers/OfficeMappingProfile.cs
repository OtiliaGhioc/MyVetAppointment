using AutoMapper;
using VetAppointment.Application.Commands;
using VetAppointment.Application.Dtos;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Helpers
{
    public class OfficeMappingProfile : Profile
    {
        public OfficeMappingProfile()
        {

            CreateMap<Office, OfficeResponse>()
                .ForMember(x => x.OfficeId, opt => opt.MapFrom(y => y.OfficeId))
                .ForMember(x => x.Address, opt => opt.MapFrom(y => y.Address))
                .ReverseMap();
            CreateMap<Office, CreateOfficeCommand>().ReverseMap();
            CreateMap<Office, UpdateOfficeCommand>().ReverseMap();
        }
    }
}
