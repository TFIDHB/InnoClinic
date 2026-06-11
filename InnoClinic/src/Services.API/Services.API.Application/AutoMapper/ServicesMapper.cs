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
            CreateMap<Service, ServiceDto>();

            CreateMap<CreateSpecializationRequestDto, Specialization>()
                .ForMember(dest => dest.Services, opt => opt.Ignore());
            CreateMap<UpdateSpecializationRequestDto, Specialization>()
                .ForMember(dest => dest.Services, opt => opt.Ignore());
            CreateMap<UpdateSpecializationStatusRequestDto, Specialization>();
            CreateMap<Specialization, SpecializationDto>();
        }
    }
}
