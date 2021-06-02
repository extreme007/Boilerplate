using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Models
{
    public class HomeViewModel
    {
        public List<ArticleViewModel> TopHot { get; set; }
        public List<ArticleViewModel> TopNew { get; set; }
        public List<ArticleViewModel> Rank1 { get; set; }
        public List<ArticleViewModel> MostViewed { get; set; }
        public List<ArticleViewModel> TheGioi { get; set; }
        public List<ArticleViewModel> TheThao { get; set; }
        public List<ArticleViewModel> XaHoi { get; set; }
        public List<ArticleViewModel> GiaiTri { get; set; }
        public List<ArticleViewModel> PhapLuat { get; set; }
    }
}
