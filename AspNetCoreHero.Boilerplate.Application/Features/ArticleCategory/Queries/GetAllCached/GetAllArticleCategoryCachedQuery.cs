using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached
{
    public class GetAllArticleCategoryCachedQuery : IRequest<Result<List<GetAllArticleCategoryCachedResponse>>>
    {
        public GetAllArticleCategoryCachedQuery()
        {
        }
    }

    public class GetAllArticlesCachedQueryHandler : IRequestHandler<GetAllArticleCategoryCachedQuery, Result<List<GetAllArticleCategoryCachedResponse>>>
    {
        private readonly IArticleCategoryCacheRepository _articleCategoryCache;
        private readonly IMapper _mapper;

        public GetAllArticlesCachedQueryHandler(IArticleCategoryCacheRepository articleCategoryCache, IMapper mapper)
        {
            _articleCategoryCache = articleCategoryCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllArticleCategoryCachedResponse>>> Handle(GetAllArticleCategoryCachedQuery request, CancellationToken cancellationToken)
        {
            var articleCategoryList = await _articleCategoryCache.GetCachedListAsync();
            var mappedArticleCategory = _mapper.Map<List<GetAllArticleCategoryCachedResponse>>(articleCategoryList);
            return Result<List<GetAllArticleCategoryCachedResponse>>.Success(mappedArticleCategory);
        }
    }
}