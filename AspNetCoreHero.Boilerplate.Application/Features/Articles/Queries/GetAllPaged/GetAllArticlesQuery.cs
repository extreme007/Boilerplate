using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged
{
    public class GetAllArticleQuery : IRequest<PaginatedResult<GetAllArticleCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? GroupCategoryId { get; set; }
        public int CategoryId { get; set; }

        public GetAllArticleQuery(int pageNumber, int pageSize,int? groupCategoryId, int categoryId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            GroupCategoryId = groupCategoryId;
            CategoryId = categoryId;
        }
    }

    public class GGetAllArticlesQueryHandler : IRequestHandler<GetAllArticleQuery, PaginatedResult<GetAllArticleCachedResponse>>
    {
        private readonly IArticleRepository _repository;

        public GGetAllArticlesQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllArticleCachedResponse>> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Article, GetAllArticleCachedResponse>> expression = e => new GetAllArticleCachedResponse
            {
                Id = e.Id,
                Title = e.Title,
                Slug= e.Slug,
                Aid = e.Aid,
                Description = e.Description,
                FullDescription = e.FullDescription,
                ThumbImage = e.ThumbImage,
                Image = e.Image,
                Link = e.Link,
                FullLink= e.FullLink,
                SourceImage= e.SourceImage,
                SourceName = e.SourceName,
                SourceLink = e.SourceLink,
                Content = e.Content,
                Author = e.Author,
                Tags= e.Tags,
                Type=e.Type,
                CategoryId= e.CategoryId,
                GroupCategoryId= e.GroupCategoryId,
                PostedDatetime= e.PostedDatetime,
                IsHot= e.IsHot,
                IsRank1= e.IsRank1,
                ViewCount= e.ViewCount,
                CommentCount= e.CommentCount,
                IsPublished=e.IsPublished,
                ArticleCategory =new GetAllArticleCategoryCachedResponse
                {
                    Id = e.ArticleCategory.Id,
                    Title = e.ArticleCategory.Title,
                    Slug = e.ArticleCategory.Title,
                    ParentId = e.ArticleCategory.ParentId,
                    Order = e.ArticleCategory.Order
                }
            };

            //List<Predicate<Article,bool>> andCriteria;

            Expression<Func<Article, bool>> expressionWhere = ar => ar.IsPublished == true && ar.ThumbImage != null;
            if(request.CategoryId == 1)
            {
                expressionWhere = expressionWhere.And(x=>x.IsHot == false);
            }
            else if(request.CategoryId == 2)
            {
                expressionWhere = expressionWhere.And(x => x.IsHot == true);


            }
            else if(request.CategoryId == 3)
            {

            }else
            {
                expressionWhere = expressionWhere.And(x=>x.GroupCategoryId == request.GroupCategoryId && x.CategoryId == request.CategoryId);
            }

            var paginatedList = await _repository.Article.Include("ArticleCategory").OrderByDescending(x=>x.PostedDatetime).Where(expressionWhere)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}