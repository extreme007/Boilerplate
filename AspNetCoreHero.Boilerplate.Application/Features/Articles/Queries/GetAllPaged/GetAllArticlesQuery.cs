using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged
{
    public class GetAllArticleQuery : IRequest<PaginatedResult<GetAllArticleResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllArticleQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllArticlesQueryHandler : IRequestHandler<GetAllArticleQuery, PaginatedResult<GetAllArticleResponse>>
    {
        private readonly IArticleRepository _repository;

        public GGetAllArticlesQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllArticleResponse>> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Article, GetAllArticleResponse>> expression = e => new GetAllArticleResponse
            {
                Id = e.Id,
                Title = e.Title,
                Slug= e.Slug,
                Aid = e.Aid,
                Description = e.Description,
                FullDescription = e.FullDescription,
                ThumbImage = e.ThumbImage,
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
                PostedDatetime= e.PostedDatetime,
                IsHot= e.IsHot,
                IsRank1= e.IsRank1,
                ViewCount= e.ViewCount,
                CommentCount= e.CommentCount,
                IsPublished=e.IsPublished
            };
            var paginatedList = await _repository.Article
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}