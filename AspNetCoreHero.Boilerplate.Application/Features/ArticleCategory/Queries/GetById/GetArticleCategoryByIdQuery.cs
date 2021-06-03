using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetById
{
    public class GetArticleCategoryByIdQuery : IRequest<Result<GetAllArticleCategoryCachedResponse>>
    {
        public int Id { get; set; }

        public class GetArticleCategoryByIdQueryHandler : IRequestHandler<GetArticleCategoryByIdQuery, Result<GetAllArticleCategoryCachedResponse>>
        {
            private readonly IArticleCategoryCacheRepository _articleCategoryCache;
            private readonly IMapper _mapper;

            public GetArticleCategoryByIdQueryHandler(IArticleCategoryCacheRepository articleCategoryCache, IMapper mapper)
            {
                _articleCategoryCache = articleCategoryCache;
                _mapper = mapper;
            }

            public async Task<Result<GetAllArticleCategoryCachedResponse>> Handle(GetArticleCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var articleCategory = await _articleCategoryCache.GetByIdAsync(query.Id);
                var mappedArticleCategory = _mapper.Map<GetAllArticleCategoryCachedResponse>(articleCategory);
                return Result<GetAllArticleCategoryCachedResponse>.Success(mappedArticleCategory);
            }
        }
    }
}