using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
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
            if(response.Succeeded)
            {
                var data = response.Data.OrderByDescending(x=>x.PostedDatetime);
                model.TopHot = _mapper.Map<List<ArticleViewModel>>(data.Where(x=>x.IsHot == true && x.IsRank1 == false).Take(10));
                model.TopNew = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.IsHot == false && x.IsRank1 == false).Take(10));
                model.Rank1 = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.IsRank1 == true).Take(5));
                model.TheGioi = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.CategoryId == 4).Take(9));
                model.XaHoi = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.CategoryId == 5).Take(9));
                model.TheThao = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.CategoryId == 21).Take(9));
                model.GiaiTri = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.CategoryId == 25).Take(9));
                model.PhapLuat = _mapper.Map<List<ArticleViewModel>>(data.Where(x => x.CategoryId == 29).Take(9));
            }    

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        [Route("{slug}",Name = "category")]
        public async Task<IActionResult> Category(string slug)
        {
            IEnumerable<ArticleViewModel> result = null;
            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            if (response.Succeeded)
            {
                List<GetAllArticleCachedResponse> dataResult = response.Data.OrderByDescending(x => x.PostedDatetime).ToList();
            
                if(!string.IsNullOrEmpty(slug))
                {
                    if(slug == "tin-moi")
                    {
                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.IsHot == false).Take(50));
                    }
                    else if(slug == "tin-nong")
                    {
                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.IsHot == true).Take(50));
                    }
                    else
                    {
                        ////var temp = _mapper.Map<List<ArticleViewModel>>(dataResult.Take(50));
                        //var categoryId = await GetIdBySlug(slug);
                        //if(categoryId != null)
                        //{
                        //    //result = GetAllChildNodesRecursivrly(categoryId, temp);
                        //   result =  _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x=>x.CategoryId == categoryId));
                        //}

                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.ArticleCategory.Slug == slug).Take(50));
                    }
                }
            }
            return View(result);
        }

        private static IEnumerable<ArticleViewModel> GetAllChildNodesRecursivrly(int? ParentId, IEnumerable<ArticleViewModel> allItems)
        {
            var allChilds = allItems.Where(i => i.CategoryId == ParentId);

            if (allChilds == null)
            {
                return new List<ArticleViewModel>();
            }

            List<ArticleViewModel> moreChildes = new List<ArticleViewModel>();
            foreach (var item in allChilds)
            {
                moreChildes.AddRange(GetAllChildNodesRecursivrly(item.Id, allItems));
            }

            return allChilds.Union(moreChildes);
        }

        private async Task<int?> GetIdBySlug(string slug)
        {
            int? result = null;
            var response = await _mediator.Send(new GetArticleCategoryBySlugQuery {Slug = slug });
            if (response.Succeeded)
            {
                if(response.Data != null)
                {
                    var dataResponse = _mapper.Map<NavigationViewModel>(response.Data);
                    result = dataResponse.Id;
                }    
             
            }
            return result;
        }

        [Route("{categorySlug}/{slug}", Name = "article")]
        public async Task<IActionResult> Article(string categorySlug, string slug)
        {
            var viewModel = new ArticleViewModel();
            if (!string.IsNullOrEmpty(slug)) {
                var slugSplit = slug.Split('-');
                var articleId = slugSplit[slugSplit.Count() - 1];
                var response = await _mediator.Send(new GetArticleByIdQuery { Id = Convert.ToInt32(articleId) });
                if (response.Succeeded)
                {
                    viewModel = _mapper.Map<ArticleViewModel>(response.Data);
                }
            }
           
            return View(viewModel);
        }
    }
}
