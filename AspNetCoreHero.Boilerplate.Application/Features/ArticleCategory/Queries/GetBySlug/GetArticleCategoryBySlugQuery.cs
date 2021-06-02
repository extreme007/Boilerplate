using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug
{
    public class GetArticleCategoryBySlugQuery : IRequest<Result<GetArticleCategoryBySlugResponse>>
    {
        public string Slug { get; set; }

        public class GetArticleCategoryBySlugQueryHandler : IRequestHandler<GetArticleCategoryBySlugQuery, Result<GetArticleCategoryBySlugResponse>>
        {
            private readonly IArticleCategoryCacheRepository _articleCategoryCache;
            private readonly IMapper _mapper;

            public GetArticleCategoryBySlugQueryHandler(IArticleCategoryCacheRepository articleCategoryCache, IMapper mapper)
            {
                _articleCategoryCache = articleCategoryCache;
                _mapper = mapper;
            }

            public async Task<Result<GetArticleCategoryBySlugResponse>> Handle(GetArticleCategoryBySlugQuery query, CancellationToken cancellationToken)
            {
                var listArticleCategory = await _articleCategoryCache.GetCachedListAsync();
                var articleCategory = listArticleCategory.FirstOrDefault(x => x.Slug == query.Slug);
                var mappedArticleCategory = _mapper.Map<GetArticleCategoryBySlugResponse>(articleCategory);
                return Result<GetArticleCategoryBySlugResponse>.Success(mappedArticleCategory);
            }
        }
    }
}