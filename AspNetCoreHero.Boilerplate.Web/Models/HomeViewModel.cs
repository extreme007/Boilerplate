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
        public List<ArticleViewModel> BreakingNews { get; set; }
        public Dictionary<int?,List<ArticleViewModel>> DataByCategory { get; set; }
        public List<NavigationViewModel> ListCategory { get; set; }
        //public List<ArticleViewModel> TheGioi { get; set; }
        //public List<ArticleViewModel> TheThao { get; set; }
        //public List<ArticleViewModel> XaHoi { get; set; }
        //public List<ArticleViewModel> GiaiTri { get; set; }
        //public List<ArticleViewModel> PhapLuat { get; set; }
        //public List<ArticleViewModel> KhoaHoc { get; set; }
        //public List<ArticleViewModel> VanHoa { get; set; }
        //public List<ArticleViewModel> KinhTe { get; set; }
        //public List<ArticleViewModel> GiaoDuc { get; set; }
        //public List<ArticleViewModel> CongNghe { get; set; }
        //public List<ArticleViewModel> DoiSong { get; set; }
        //public List<ArticleViewModel> XeCo { get; set; }
        //public List<ArticleViewModel> NhaDat { get; set; }
    }
}
