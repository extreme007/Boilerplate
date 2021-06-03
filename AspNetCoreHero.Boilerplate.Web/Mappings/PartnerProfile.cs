using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Boilerplate.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Mappings
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<Partner, PartnerViewModel>().ReverseMap();
            CreateMap<GetAllPartnerCachedResponse, PartnerViewModel>().ReverseMap();
        }
    }
}
