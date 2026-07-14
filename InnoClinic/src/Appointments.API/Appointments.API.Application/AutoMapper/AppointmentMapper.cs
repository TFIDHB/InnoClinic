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
        }
    }
}
