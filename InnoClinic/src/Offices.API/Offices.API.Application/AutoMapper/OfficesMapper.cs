using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class OfficesMapper : Profile
    {
        public OfficesMapper()
        {
            CreateMap<CreateOfficeRequestDto, Office>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateOfficeRequestDto, Office>();
            CreateMap<UpdateOfficeStatusRequestDto, Office>();
            CreateMap<Office, OfficeDto>();
        }
    }
}
