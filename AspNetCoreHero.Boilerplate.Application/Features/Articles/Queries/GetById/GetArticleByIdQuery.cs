using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById
{
    public class GetArticleByIdQuery : IRequest<Result<GetAllArticleCachedResponse>>
    {
        public int Id { get; set; }

        public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Result<GetAllArticleCachedResponse>>
        {
            private readonly IArticleCacheRepository _articleCache;
            private readonly IArticleCategoryCacheRepository _articleCategoryCache;
            private readonly IMapper _mapper;

            public GetArticleByIdQueryHandler(IArticleCacheRepository articleCache, IArticleCategoryCacheRepository articleCategoryCache, IMapper mapper)
            {
                _articleCache = articleCache;
                _articleCategoryCache = articleCategoryCache;
                _mapper = mapper;
            }

            public async Task<Result<GetAllArticleCachedResponse>> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
            {
                var article = await _articleCache.GetByIdAsync(query.Id);
                article.ArticleCategory = await _articleCategoryCache.GetByIdAsync(article.CategoryId);
                var mappedArticle = _mapper.Map<GetAllArticleCachedResponse>(article);
                return Result<GetAllArticleCachedResponse>.Success(mappedArticle);
            }
        }
    }
}