using System;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached
{
    public class GetAllArticleCategoryCachedResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
    }
}