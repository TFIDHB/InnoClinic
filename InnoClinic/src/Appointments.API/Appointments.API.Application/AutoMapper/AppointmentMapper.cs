using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.AutoMapper
{
    public class AppointmentMapper : Profile
    {
        public AppointmentMapper()
        {
            CreateMap<CreateAppointmentRequestDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.Created))
                .ForMember(dest => dest.Duration, opt => opt.Ignore());

            CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Appointment, AppointmentSlotDto>();

            CreateMap<Appointment, ScheduleDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Time.Add(src.Duration)))
                .ForMember(dest => dest.PatientId, opt => opt.Ignore())
                .ForMember(dest => dest.PatientFullName, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceName, opt => opt.Ignore())
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Status == AppointmentStatus.Approved))
                .ForMember(dest => dest.HasResult, opt => opt.MapFrom(src => false));

            CreateMap<Appointment, AppointmentListItemDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Time.Add(src.Duration)))
                .ForMember(dest => dest.DoctorFullName, opt => opt.Ignore())
                .ForMember(dest => dest.PatientFullName, opt => opt.Ignore())
                .ForMember(dest => dest.PatientPhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceName, opt => opt.Ignore())
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Status == AppointmentStatus.Approved));
        }
    }
}
