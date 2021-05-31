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

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllPaged
{
    public class GetAllArticleCategoryQuery : IRequest<PaginatedResult<GetAllArticleCategoryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllArticleCategoryQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllArticleCategoryQueryHandler : IRequestHandler<GetAllArticleCategoryQuery, PaginatedResult<GetAllArticleCategoryResponse>>
    {
        private readonly IArticleCategoryRepository _repository;

        public GGetAllArticleCategoryQueryHandler(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllArticleCategoryResponse>> Handle(GetAllArticleCategoryQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AspNetCoreHero.Boilerplate.Domain.Entities.ArticleCategory, GetAllArticleCategoryResponse>> expression = e => new GetAllArticleCategoryResponse
            {
                Id = e.Id,
                Title = e.Title,
                Slug= e.Slug,
                ParentId = e.ParentId,
                Order=e.Order

            };
            var paginatedList = await _repository.ArticleCategory
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}