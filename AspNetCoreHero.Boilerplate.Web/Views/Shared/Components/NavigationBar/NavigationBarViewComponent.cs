using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Controllers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.NavigationBar
{
    public class NavigationBarViewComponent : BaseViewComponent<NavigationBarViewComponent>
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new List<NavigationViewModel>();
            var response = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
            if(response.Succeeded)
            {
                viewModel = _mapper.Map<List<NavigationViewModel>>(response.Data.OrderBy(x=>x.Order));
            }
            return View(viewModel);
        }
    }
}