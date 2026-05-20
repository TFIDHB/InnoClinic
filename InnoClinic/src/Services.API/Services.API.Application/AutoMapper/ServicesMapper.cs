using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.AutoMapper
{
    public class ServicesMapper : Profile
    {
        public ServicesMapper() {

            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateServiceRequestDto, Service>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ServiceStatus.Active));

            CreateMap<UpdateServiceRequestDto, Service>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
