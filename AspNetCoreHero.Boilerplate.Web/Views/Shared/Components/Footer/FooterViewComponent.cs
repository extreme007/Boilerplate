using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Partners.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Controllers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.Footer
{
    public class FooterViewComponent : BaseViewComponent<ViewComponent>
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new FooterViewModel();
            var listPartner  = await _mediator.Send(new GetAllPartnerCachedQuery());
            var listCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
            if (listPartner.Succeeded && listCategory.Succeeded)
            {
                viewModel.Partner = _mapper.Map<List<PartnerViewModel>>(listPartner.Data);
                viewModel.Category = _mapper.Map<List<NavigationViewModel>>(listCategory.Data.OrderBy(x=>x.Order).Where(x=>x.ParentId== null && x.IsVisible ==true));
            }
            return View(viewModel);
        }
    }
}