using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById
{
    public class GetArticleByIdQuery : IRequest<Result<GetArticleByIdResponse>>
    {
        public int Id { get; set; }

        public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Result<GetArticleByIdResponse>>
        {
            private readonly IArticleCacheRepository _articleCache;
            private readonly IMapper _mapper;

            public GetArticleByIdQueryHandler(IArticleCacheRepository articleCache, IMapper mapper)
            {
                _articleCache = articleCache;
                _mapper = mapper;
            }

            public async Task<Result<GetArticleByIdResponse>> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
            {
                var article = await _articleCache.GetByIdAsync(query.Id);
                var mappedArticle = _mapper.Map<GetArticleByIdResponse>(article);
                return Result<GetArticleByIdResponse>.Success(mappedArticle);
            }
        }
    }
}