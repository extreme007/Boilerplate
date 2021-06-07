using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
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
    public class GetAllArticlesNoCacheQuery : IRequest<Result<List<GetAllArticleCachedResponse>>>
    {
        public GetAllArticlesNoCacheQuery()
        {
        }
    }

    public class GetAllArticlesNoCacheQueryHandler : IRequestHandler<GetAllArticlesNoCacheQuery, Result<List<GetAllArticleCachedResponse>>>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllArticlesNoCacheQueryHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllArticleCachedResponse>>> Handle(GetAllArticlesNoCacheQuery request, CancellationToken cancellationToken)
        {
            var articleList = await _repository.GetListAsync("ArticleCategory");
            var mappedArticles = _mapper.Map<List<GetAllArticleCachedResponse>>(articleList.OrderByDescending(x => x.PostedDatetime).Where(x => x.IsPublished == true && x.ThumbImage != null).Take(200));
            return Result<List<GetAllArticleCachedResponse>>.Success(mappedArticles);
        }
    }
}