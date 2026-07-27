using Application.DTOs;
using AutoMapper;
using BLL.DTOs;
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
            CreateMap<CreatePatientProfileRequestDto, PatientProfile>();
            CreateMap<UpdatePatientProfileRequestDto, PatientProfile>();
            CreateMap<UserAccountInfoDto, PatientProfileDto>();
            CreateMap<CreateMyPatientProfileRequestDto, PatientProfile>();

            CreateMap<ReceptionistProfile, ReceptionistProfileDto>();
            CreateMap<CreateReceptionistProfileRequestDto, ReceptionistProfile>();
            CreateMap<UpdateReceptionistProfileRequestDto, ReceptionistProfile>();
        }
    }
}
