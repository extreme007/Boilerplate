using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached
{
    public class GetAllArticleCachedQuery : IRequest<Result<List<GetAllArticleCachedResponse>>>
    {
        public GetAllArticleCachedQuery()
        {
        }
    }

    public class GetAllArticlesCachedQueryHandler : IRequestHandler<GetAllArticleCachedQuery, Result<List<GetAllArticleCachedResponse>>>
    {
        private readonly IArticleCacheRepository _articleCache;
        private readonly IMapper _mapper;

        public GetAllArticlesCachedQueryHandler(IArticleCacheRepository articleCache, IMapper mapper)
        {
            _articleCache = articleCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllArticleCachedResponse>>> Handle(GetAllArticleCachedQuery request, CancellationToken cancellationToken)
        {
            var articleList = await _articleCache.GetCachedListAsync("ArticleCategory");
            var mappedArticles = _mapper.Map<List<GetAllArticleCachedResponse>>(articleList.Where(x=>x.IsPublished ==true && x.ThumbImage != null));
            return Result<List<GetAllArticleCachedResponse>>.Success(mappedArticles);
        }
    }
}