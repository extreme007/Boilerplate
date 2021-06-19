using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetByCategoryId
{
    public class GetArticleByCategoryIdQuery : IRequest<PaginatedResult<GetAllArticleCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? GroupCategoryId { get; set; }
        public int CategoryId { get; set; }

        public GetArticleByCategoryIdQuery(int pageNumber, int pageSize, int? groupCategoryId, int categoryId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            GroupCategoryId = groupCategoryId;
            CategoryId = categoryId;
        }
    }

    public class GetArticleByCategoryIdQueryHandler : IRequestHandler<GetArticleByCategoryIdQuery,PaginatedResult<GetAllArticleCachedResponse>>
    {
        private readonly IArticleCacheRepository _articleCache;
        private readonly IMapper _mapper;

        public GetArticleByCategoryIdQueryHandler(IArticleCacheRepository articleCache, IMapper mapper)
        {
            _articleCache = articleCache;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllArticleCachedResponse>> Handle(GetArticleByCategoryIdQuery query, CancellationToken cancellationToken)
        {
            //string cacheKey = ArticleCacheKeys.ListKey;
            //var articleList = await _cacheService.GetAsync<List<Article>>(cacheKey);

            //var listArticle = await _articleCache.GetCachedListAsync("ArticleCategory");
            //Func<Article, bool> expressionWhere = x => x.IsPublished == true && x.ThumbImage != null && x.GroupCategoryId == query.GroupCategoryId && x.CategoryId == query.CategoryId;
            //var listFilter = listArticle.Where(expressionWhere).OrderByDescending(x=>x.PostedDatetime);
            var listFilter = await _articleCache.GetByGroupCategoryIdAsync(query.GroupCategoryId.Value, query.CategoryId, "ArticleCategory");
            var total = listFilter.Count();
            var listResult = listFilter.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);
            var mappedArticle = _mapper.Map<List<GetAllArticleCachedResponse>>(listResult);
            return PaginatedResult<GetAllArticleCachedResponse>.Success(mappedArticle,total,query.PageNumber,query.PageSize);
        }
    }
}