using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetBySlug
{
    public class GetPartnerBySlugQuery : IRequest<Result<GetAllPartnerCachedResponse>>
    {
        public string Slug { get; set; }

        public class GetPartnerBySlugQueryHandler : IRequestHandler<GetPartnerBySlugQuery, Result<GetAllPartnerCachedResponse>>
        {
            private readonly IPartnerCacheRepository _PartnerCache;
            private readonly IMapper _mapper;

            public GetPartnerBySlugQueryHandler(IPartnerCacheRepository PartnerCache, IMapper mapper)
            {
                _PartnerCache = PartnerCache;
                _mapper = mapper;
            }

            public async Task<Result<GetAllPartnerCachedResponse>> Handle(GetPartnerBySlugQuery query, CancellationToken cancellationToken)
            {
                var listPartner = await _PartnerCache.GetCachedListAsync();
                var Partner = listPartner.FirstOrDefault(x => x.Slug == query.Slug);
                var mappedPartner = _mapper.Map<GetAllPartnerCachedResponse>(Partner);
                return Result<GetAllPartnerCachedResponse>.Success(mappedPartner);
            }
        }
    }
}