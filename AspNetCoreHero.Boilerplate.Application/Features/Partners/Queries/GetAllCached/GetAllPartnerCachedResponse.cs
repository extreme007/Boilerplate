using System;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached
{
    public class GetAllPartnerCachedResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
    }
}