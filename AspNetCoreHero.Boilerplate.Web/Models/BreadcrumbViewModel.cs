using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Models
{
    public class BreadcrumbViewModel
    {
        public List<NavigationViewModel> ListBreadcrumb { get; set; } = new List<NavigationViewModel>();
        public bool IsArticle { get; set; }
    }
}
