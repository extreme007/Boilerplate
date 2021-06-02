using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    internal class ArticleCategoryProfile : Profile
    {
        public ArticleCategoryProfile()
        {
            CreateMap<CreateArticleCategoryCommand, ArticleCategory>().ReverseMap();
            CreateMap<GetArticleCategoryByIdResponse, ArticleCategory>().ReverseMap();
            CreateMap<GetAllArticleCategoryCachedResponse, ArticleCategory>().ReverseMap();
            CreateMap<GetAllArticleCategoryResponse, ArticleCategory>().ReverseMap();
            CreateMap<GetArticleCategoryBySlugResponse, ArticleCategory>().ReverseMap();
        }
    }
}