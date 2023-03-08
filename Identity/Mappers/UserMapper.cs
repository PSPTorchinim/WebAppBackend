using AutoMapper;
using Identity.DTO.User;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() {
            CreateMap<User, GetUsersDTO>();
        }
    }
}
