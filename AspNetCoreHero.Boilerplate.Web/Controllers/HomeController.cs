using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetByCategoryId;
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
    public class HomeController :BaseController<HomeController>
    {
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            var responseCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
           
            //var response = await _mediator.Send(new GetAllArticlesNoCacheQuery());
            if (response.Succeeded && responseCategory.Succeeded)
            {
                var data = _mapper.Map<List<ArticleViewModel>>(response.Data);
                model.TopHot = data.Where(x=>x.IsHot == true && x.IsRank1 == false).Take(10).ToList();
                model.TopNew = data.Where(x => x.IsHot == false && x.IsRank1 == false).Take(10).ToList();
                model.Rank1 =  data.Where(x => x.IsRank1 == true).Take(4).ToList();
                model.BreakingNews = data.Take(10).ToList();
                model.DataByCategory = data.GroupBy(x => x.GroupCategoryId).ToDictionary(x => x.Key, x=>x.ToList());
                model.ListCategory = _mapper.Map<List<NavigationViewModel>>(responseCategory.Data);
            }    

            return View(model);
        }
    }
}
