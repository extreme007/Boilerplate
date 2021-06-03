using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllPaged
{
    public class GetAllPartnerQuery : IRequest<PaginatedResult<GetAllPartnerCachedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllPartnerQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetPartnerQueryHandler : IRequestHandler<GetAllPartnerQuery, PaginatedResult<GetAllPartnerCachedResponse>>
    {
        private readonly IPartnerRepository _repository;

        public GetPartnerQueryHandler(IPartnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllPartnerCachedResponse>> Handle(GetAllPartnerQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AspNetCoreHero.Boilerplate.Domain.Entities.Partner, GetAllPartnerCachedResponse>> expression = e => new GetAllPartnerCachedResponse
            {
                Id = e.Id,
                Title = e.Title,
                Slug= e.Slug,
                Link = e.Link,
                Image = e.Image,
                Order=e.Order

            };
            var paginatedList = await _repository.Partners
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}