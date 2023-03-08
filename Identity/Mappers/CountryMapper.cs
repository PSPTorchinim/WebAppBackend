using AutoMapper;
using Identity.DTO.Country;
using Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Mappers
{
    public class CountryMapper : Profile
    {
        public CountryMapper() {
            CreateMap<Country, GetCountryDTO>();
        }
    }
}
