using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Controllers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.Sidebar
{
    public class SidebarViewComponent : BaseViewComponent<ViewComponent>
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new SidebarViewModel();
            var responseArticle = await _mediator.Send(new GetAllArticleCachedQuery());
            var responseCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
            if (responseArticle.Succeeded && responseCategory.Succeeded)
            {
                var dataArticle = _mapper.Map<List<ArticleViewModel>>(responseArticle.Data);
                viewModel.SidebarCategory = _mapper.Map<List<NavigationViewModel>>(responseCategory.Data.Where(x=>x.ParentId == null && x.IsVisible == true).OrderBy(x => x.Order));
                viewModel.SidebarTopNews = dataArticle.Take(5).ToList();
                viewModel.SidebarTopHot = dataArticle.Where(x=>x.IsHot ==true).Take(5).ToList();
                viewModel.SidebarTopView = dataArticle.OrderByDescending(x => x.ViewCount).Take(5).ToList();
                viewModel.SiebarTopComment = dataArticle.OrderByDescending(x => x.CommentCount).Take(5).ToList();
            }
            return View(viewModel);
        }
    }
}