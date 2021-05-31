using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.NavigationBar
{
    public class NavigationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}