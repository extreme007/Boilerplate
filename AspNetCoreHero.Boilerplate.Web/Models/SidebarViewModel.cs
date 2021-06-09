using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Models
{
    public class SidebarViewModel
    {
        public List<ArticleViewModel> SidebarTopNews { get; set; }
        public List<ArticleViewModel> SidebarTopHot { get; set; }
        public List<ArticleViewModel> SiebarTopComment { get; set; }
        public List<ArticleViewModel> SidebarTopView { get; set; }
        public List<NavigationViewModel> SidebarCategory { get; set; }
    }
}
