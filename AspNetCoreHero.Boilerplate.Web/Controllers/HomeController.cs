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
            //var response = await _mediator.Send(new GetAllArticlesNoCacheQuery());
            if (response.Succeeded)
            {
                var data = response.Data;
                model.TopHot = _mapper.Map<List<ArticleViewModel>>(data.Where(x=>x.IsHot == true && x.IsRank1 == false).Take(10));
                model.TopNew = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.IsHot == false && x.IsRank1 == false).Take(10));
                model.Rank1 = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.IsRank1 == true).Take(4));
                model.TheGioi = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.GroupCategoryId == 4).Take(6));
                model.XaHoi = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.GroupCategoryId == 5));
                model.TheThao = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.GroupCategoryId == 21).Take(6));
                model.PhapLuat = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.GroupCategoryId == 29).Take(6));
                model.GiaiTri = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.GroupCategoryId == 25).Take(6));

                model.BreakingNews = _mapper.Map<List<ArticleViewModel>>(data.Take(10));
            }    

            return View(model);
        }
    }
}
