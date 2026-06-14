using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class ServicesMapper : Profile
    {
        public ServicesMapper()
        {
            CreateMap<CreateServiceRequestDto, Service>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateServiceRequestDto, Service>();
            CreateMap<UpdateServiceStatusRequestDto, Service>();
            CreateMap<Service, ServiceDto>();

            CreateMap<CreateSpecializationRequestDto, Specialization>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<UpdateSpecializationRequestDto, Specialization>();
            CreateMap<UpdateSpecializationStatusRequestDto, Specialization>();
            CreateMap<Specialization, SpecializationDto>();
        }
    }
}
