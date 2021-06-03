using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached
{
    public class GetAllPartnerCachedQuery : IRequest<Result<List<GetAllPartnerCachedResponse>>>
    {
        public GetAllPartnerCachedQuery()
        {
        }
    }

    public class GetAllOPartnerCachedQueryHandler : IRequestHandler<GetAllPartnerCachedQuery, Result<List<GetAllPartnerCachedResponse>>>
    {
        private readonly IPartnerCacheRepository _PartnerCache;
        private readonly IMapper _mapper;

        public GetAllOPartnerCachedQueryHandler(IPartnerCacheRepository PartnerCache, IMapper mapper)
        {
            _PartnerCache = PartnerCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllPartnerCachedResponse>>> Handle(GetAllPartnerCachedQuery request, CancellationToken cancellationToken)
        {
            var PartnerList = await _PartnerCache.GetCachedListAsync();
            var mappedPartner = _mapper.Map<List<GetAllPartnerCachedResponse>>(PartnerList);
            return Result<List<GetAllPartnerCachedResponse>>.Success(mappedPartner);
        }
    }
}