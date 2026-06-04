using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class ProfilesMapper : Profile
    {
        public ProfilesMapper() 
        {
            CreateMap<DoctorProfile, DoctorProfileDto>();
            CreateMap<CreateDoctorProfileRequestDto, DoctorProfile>();
            CreateMap<UpdateDoctorProfileRequestDto, DoctorProfile>();

            CreateMap<PatientProfile, PatientProfileDto>();
            CreateMap<CreatePatientProfileRequestDto, PatientProfile>()
                .ForMember(dest => dest.IsLinkedToAccount, opt => opt.MapFrom(_ => false));
            CreateMap<UpdatePatientProfileRequestDto, PatientProfile>();

            CreateMap<ReceptionistProfile, ReceptionistProfileDto>();
            CreateMap<CreateReceptionistProfileRequestDto, ReceptionistProfile>();
            CreateMap<UpdateReceptionistProfileRequestDto, ReceptionistProfile>();
        }
    }
}
