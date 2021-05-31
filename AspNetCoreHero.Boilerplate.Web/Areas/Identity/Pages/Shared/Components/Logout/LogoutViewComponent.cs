using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Identity.Views.Shared.Components.Logout
{
    public class LogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}