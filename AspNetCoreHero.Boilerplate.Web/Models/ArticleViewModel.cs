using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Models
{
    public class ArticlePagingViewModel
    {
        public List<ArticleViewModel> Data { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }
        //public bool HasPreviousPage { get; }
        //public bool HasNextPage { get; }
    }

    public class ArticleViewModel
    {
        public int Id { get; set; }
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
        public int? GroupCategoryId { get; set; }
        public DateTime PostedDatetime { get; set; }
        public bool IsHot { get; set; }
        public bool IsRank1 { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public bool IsPublished { get; set; }
        public NavigationViewModel ArticleCategory { get; set; }
        public string FullSlug
        {
            get
            {
                return string.Format("/{0}/{1}-{2}", this.ArticleCategory.Slug, this.Slug, this.Id);
            }
        }
        public string PostedDatetimeString {
            get
            {
                return PostedDatetime.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
