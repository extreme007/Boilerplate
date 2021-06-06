using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
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
    public class GetArticleByCategoryIdQuery : IRequest<Result<List<GetAllArticleCachedResponse>>>
    {
        public int CategoryId { get; set; }

        public class GetArticleByCategoryIdQueryHandler : IRequestHandler<GetArticleByCategoryIdQuery, Result<List<GetAllArticleCachedResponse>>>
        {
            private readonly IArticleCacheRepository _articleCache;
            private readonly IMapper _mapper;

            public GetArticleByCategoryIdQueryHandler(IArticleCacheRepository articleCache, IMapper mapper)
            {
                _articleCache = articleCache;
                _mapper = mapper;
            }

            public async Task<Result<List<GetAllArticleCachedResponse>>> Handle(GetArticleByCategoryIdQuery query, CancellationToken cancellationToken)
            {
                var article = await _articleCache.GetCachedListAsync("ArticleCategory");
                var listResult = article.Where(x => x.CategoryId == query.CategoryId && x.IsPublished ==true);
                var mappedArticle = _mapper.Map<List<GetAllArticleCachedResponse>>(listResult);
                return Result<List<GetAllArticleCachedResponse>>.Success(mappedArticle);
            }
        }
    }
}