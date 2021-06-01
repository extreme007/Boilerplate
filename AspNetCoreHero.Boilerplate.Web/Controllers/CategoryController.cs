using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetByCategoryId;
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
    public class CategoryController : BaseController<CategoryController>
    {
        public async Task<IActionResult> Index(int Id)
        {
            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            if (response.Succeeded)
            {
                var dataResult = response.Data.OrderByDescending(x => x.PostedDatetime).Take(50);
                var viewModel = _mapper.Map<List<ArticleViewModel>>(dataResult);
                return View(viewModel);
            }
            return View(null);
        }
    }
}
