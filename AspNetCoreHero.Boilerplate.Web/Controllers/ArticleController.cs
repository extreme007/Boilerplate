using AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById;
using AspNetCoreHero.Boilerplate.Web.Extensions;
using AspNetCoreHero.Boilerplate.Web.Filters;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Controllers
{
    [AllowAnonymous]
    public class ArticleController : BaseController<ArticleController>
    {
        [PageVisitCountFilter]
        [Route("{slug}-{id}.html", Name = "article")]
        public async Task<IActionResult> Index(string slug,string id)
        {
            int pageId = HttpContext.Session.Get<int>("VisitedURL");
            if (pageId > 0)
            {
                var result = await UpdatePageViews(pageId);
            }

            var viewModel = new ArticleViewModel();
            if (!string.IsNullOrEmpty(slug))
            {
                var response = await _mediator.Send(new GetArticleByIdQuery { Id = Convert.ToInt32(id) });
                if (response.Succeeded)
                {
                    viewModel = _mapper.Map<ArticleViewModel>(response.Data);
                    ViewData["Category"] = viewModel.ArticleCategory;
                }
            }

            return View(viewModel);
        }

        private async Task<int> UpdatePageViews(int pageId)
        {
            var response = await _mediator.Send(new UpdateViewCountCommand { Id = pageId });
            return response.Data;
        }
    }
}
