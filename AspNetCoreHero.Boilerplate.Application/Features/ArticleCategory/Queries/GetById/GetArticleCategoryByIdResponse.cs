namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetById
{
    public class GetArticleCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
    }
}