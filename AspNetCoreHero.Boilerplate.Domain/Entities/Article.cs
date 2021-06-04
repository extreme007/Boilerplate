using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Domain.Entities
{
    public class Article : AuditableEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Aid { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string ThumbImage { get; set; }
        public string Image { get; set; }
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
        public int GroupCategoryId { get; set; }
        public DateTime PostedDatetime { get; set; }
        public bool IsHot { get; set; }
        public bool IsRank1 { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public bool IsPublished { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}
