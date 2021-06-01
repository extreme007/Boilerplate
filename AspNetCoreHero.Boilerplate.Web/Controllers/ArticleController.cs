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
        public async Task<IActionResult> Index(int Id)
        {
            var response = await _mediator.Send(new GetArticleByIdQuery {Id = Id });
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<ArticleViewModel>(response.Data);
                return View(viewModel);
            }
            return View(null);
        }
    }
}
