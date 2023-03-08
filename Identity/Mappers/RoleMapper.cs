using AutoMapper;
using Identity.DTO.Role;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper() {
            CreateMap<Role, GetRolesDTO>();
        }
    }
}
