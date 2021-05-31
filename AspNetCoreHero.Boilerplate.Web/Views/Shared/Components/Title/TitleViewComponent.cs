using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.Title
{
    public class NavigationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}