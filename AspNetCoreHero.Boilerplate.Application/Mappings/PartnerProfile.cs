using AspNetCoreHero.Boilerplate.Application.Features.Partners.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    internal class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<CreatePartnerCommand, Partner>().ReverseMap();
            CreateMap<GetAllPartnerCachedResponse, Partner>().ReverseMap();
        }
    }
}