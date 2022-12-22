using AutoMapper;
using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;
using VetAppointment.WebAPI.Dtos.UserDto;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateDrugDto, Drug>()
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title));
            CreateMap<Drug, DrugDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.DrugId))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title));
            CreateMap<CreateDrugStockDto, DrugStock>()
                .ForMember(x => x.DrugStockId, opt => opt.Ignore())
                .ForMember(x => x.Type, opt => opt.Ignore());
            CreateMap<DrugStock, DrugStockDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.DrugStockId));
            CreateMap<DefaultUserDto, User>()
                .ForMember(x => x.IsMedic, opt => opt.Ignore())
                .ForMember(x => x.HasOffice, opt => opt.Ignore())
                .ForMember(x => x.OfficeId, opt => opt.Ignore())
                .ForMember(x => x.UserOffice, opt => opt.Ignore())
                .ForMember(x => x.JoinedDate, opt => opt.Ignore());
            CreateMap<User, CompleteUserDto>()
                .ForMember(x => x.IsMedic, opt => opt.MapFrom(y => y.IsMedic))
                .ForMember(x => x.HasOffice, opt => opt.MapFrom(y => y.HasOffice))
                .ForMember(x => x.OfficeId, opt => opt.MapFrom(y => y.OfficeId))
                .ForMember(x => x.JoinedDate, opt => opt.MapFrom(y => y.JoinedDate));
            CreateMap<OfficeDto, Office>()
                .ForMember(x => x.OfficeId, opt => opt.MapFrom(y => y.OfficeId))
                .ForMember(x => x.Address, opt => opt.MapFrom(y => y.Address));
            CreateMap<Office, OfficeDto>()
                .ForMember(x => x.OfficeId, opt => opt.MapFrom(y => y.OfficeId))
                .ForMember(x => x.Address, opt => opt.MapFrom(y => y.Address));
            CreateMap<MedicalEntryCreateDto, MedicalHistoryEntry>()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(y => y.AppointmentId))
                .ForMember(x => x.PrescriptionId, opt => opt.MapFrom(y => y.PrescriptionId));
            CreateMap<MedicalHistoryEntry, MedicalEntryDetailDto>()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(y => y.AppointmentId))
                .ForMember(x => x.PrescriptionId, opt => opt.MapFrom(y => y.PrescriptionId));
            CreateMap<BillingEntryDto, BillingEntry>()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(y => y.AppointmentId))
                .ForMember(x => x.PrescriptionId, opt => opt.MapFrom(y => y.PrescriptionId))
                .ForMember(x => x.BillingEntryId, opt => opt.MapFrom(y => y.BillingEntryId))
                .ForMember(x => x.IssuerId, opt => opt.MapFrom(y => y.IssuerId))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(y => y.CustomerId))
                .ForMember(x => x.DateTime, opt => opt.MapFrom(y => y.DateTime))
                .ForMember(x => x.Price, opt => opt.MapFrom(y => y.Price));
            CreateMap<BillingEntry, BillingEntryDto>()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(y => y.AppointmentId))
                .ForMember(x => x.PrescriptionId, opt => opt.MapFrom(y => y.PrescriptionId))
                .ForMember(x => x.BillingEntryId, opt => opt.MapFrom(y => y.BillingEntryId))
                .ForMember(x => x.IssuerId, opt => opt.MapFrom(y => y.IssuerId))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(y => y.CustomerId))
                .ForMember(x => x.DateTime, opt => opt.MapFrom(y => y.DateTime))
                .ForMember(x => x.Price, opt => opt.MapFrom(y => y.Price));
            CreateMap<AppointmentCreateDto, Appointment>()
                .ForMember(x => x.AppointerId, opt => opt.MapFrom(y => y.AppointerId))
                .ForMember(x => x.AppointeeId, opt => opt.MapFrom(y => y.AppointeeId))
                .ForMember(x => x.DueDate, opt => opt.MapFrom(y => y.DueDate))
                .ForMember(x => x.Description, opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.Type, opt => opt.MapFrom(y => y.Type));
            CreateMap<Appointment, AppointmentDetailDto>()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(y => y.AppointmentId))
                .ForMember(x => x.AppointerId, opt => opt.MapFrom(y => y.AppointerId))
                .ForMember(x => x.AppointeeId, opt => opt.MapFrom(y => y.AppointeeId))
                .ForMember(x => x.DueDate, opt => opt.MapFrom(y => y.DueDate))
                .ForMember(x => x.Description, opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.Type, opt => opt.MapFrom(y => y.Type))
                .ForMember(x => x.IsExpired, opt => opt.MapFrom(y => y.IsExpired))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(y => y.CreatedAt));
            CreateMap<AppointmentModifyDto, Appointment>()
                .ForMember(x => x.DueDate, opt => opt.MapFrom(y => y.DueDate))
                .ForMember(x => x.Description, opt => opt.MapFrom(y => y.Description))
                .ForMember(x => x.Title, opt => opt.MapFrom(y => y.Title))
                .ForMember(x => x.Type, opt => opt.MapFrom(y => y.Type))
                .ForMember(x => x.IsExpired, opt => opt.MapFrom(y => y.IsExpired));
        }
    }
}
