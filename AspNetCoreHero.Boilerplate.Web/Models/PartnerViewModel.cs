using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Models
{
    public class FooterViewModel
    {
        public List<PartnerViewModel> Partner { get; set; }
        public List<NavigationViewModel> Category { get; set; }
    }
    public class PartnerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
    }
}
