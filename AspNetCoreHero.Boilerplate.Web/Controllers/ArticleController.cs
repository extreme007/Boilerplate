using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetById;
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
        [Route("{slug}-{id}.html", Name = "article")]
        public async Task<IActionResult> Index(string slug,string id)
        {
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
    }
}
