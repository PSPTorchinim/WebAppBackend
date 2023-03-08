using AutoMapper;
using Identity.DTO.Permission;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper() {
            CreateMap<Permission, GetPermissionsDTO>();
        }
    }
}
