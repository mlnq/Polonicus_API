using AutoMapper;
using Polonicus_API.Entities;
using Polonicus_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polonicus_API
{
    public class PolonicusMappingProfile : Profile
    {
        public PolonicusMappingProfile()
        {
            CreateMap<Outpost, OutpostDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<CreateOutpostDto, Outpost>()
                .ForMember(o => o.Address,
                    c => c.MapFrom(dto => new Address()
                    { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode })); 


            CreateMap<Chronicle, ChronicleDto>();
            CreateMap<CreateChronicleDto, Chronicle>()
                .ForMember(
                    m => m.PublicationDate, p => p.MapFrom(s => DateTime.ParseExact(s.PublicationDate,"yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture))
                );

            CreateMap<OutpostDto, Outpost>();
        }
    }
}
