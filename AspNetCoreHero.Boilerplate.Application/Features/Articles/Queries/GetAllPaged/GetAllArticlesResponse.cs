using System;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged
{
    public class GetAllArticleResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Aid { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string ThumbImage { get; set; }
        public string Link { get; set; }
        public string FullLink { get; set; }
        public string SourceImage { get; set; }
        public string SourceName { get; set; }
        public string SourceLink { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public DateTime PostedDatetime { get; set; }
        public bool IsHot { get; set; }
        public bool IsRank1 { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public bool IsPublished { get; set; }
    }
}