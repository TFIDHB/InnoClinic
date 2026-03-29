using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper() {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(p => p.PasswordHash, o => o.Ignore());
        }
    }
}
