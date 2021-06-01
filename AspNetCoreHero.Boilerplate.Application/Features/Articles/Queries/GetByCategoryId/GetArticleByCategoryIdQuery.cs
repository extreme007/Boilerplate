using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetByCategoryId
{
    public class GetArticleByCategoryIdQuery : IRequest<Result<List<GetArticleByCategoryIdResponse>>>
    {
        public int CategoryId { get; set; }

        public class GetArticleByCategoryIdQueryHandler : IRequestHandler<GetArticleByCategoryIdQuery, Result<List<GetArticleByCategoryIdResponse>>>
        {
            private readonly IArticleCacheRepository _articleCache;
            private readonly IMapper _mapper;

            public GetArticleByCategoryIdQueryHandler(IArticleCacheRepository articleCache, IMapper mapper)
            {
                _articleCache = articleCache;
                _mapper = mapper;
            }

            public async Task<Result<List<GetArticleByCategoryIdResponse>>> Handle(GetArticleByCategoryIdQuery query, CancellationToken cancellationToken)
            {
                var article = await _articleCache.GetCachedListAsync();
                var listResult = article.Where(x => x.CategoryId == query.CategoryId);
                var mappedArticle = _mapper.Map<List<GetArticleByCategoryIdResponse>>(listResult);
                return Result<List<GetArticleByCategoryIdResponse>>.Success(mappedArticle);
            }
        }
    }
}