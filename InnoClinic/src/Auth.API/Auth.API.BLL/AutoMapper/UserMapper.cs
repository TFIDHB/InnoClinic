using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(p => p.PasswordHash, o => o.Ignore());
        }
    }
}
