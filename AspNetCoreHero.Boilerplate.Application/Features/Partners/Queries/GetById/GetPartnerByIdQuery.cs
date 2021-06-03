using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetById
{
    public class GetPartnerByIdQuery : IRequest<Result<GetAllPartnerCachedResponse>>
    {
        public int Id { get; set; }

        public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, Result<GetAllPartnerCachedResponse>>
        {
            private readonly IPartnerCacheRepository _PartnerCache;
            private readonly IMapper _mapper;

            public GetPartnerByIdQueryHandler(IPartnerCacheRepository PartnerCache, IMapper mapper)
            {
                _PartnerCache = PartnerCache;
                _mapper = mapper;
            }

            public async Task<Result<GetAllPartnerCachedResponse>> Handle(GetPartnerByIdQuery query, CancellationToken cancellationToken)
            {
                var Partner = await _PartnerCache.GetByIdAsync(query.Id);
                var mappedPartner = _mapper.Map<GetAllPartnerCachedResponse>(Partner);
                return Result<GetAllPartnerCachedResponse>.Success(mappedPartner);
            }
        }
    }
}