using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug;
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
        [Route("{slug}", Name = "category")]
        public async Task<IActionResult> Index(string slug)
        {
            IEnumerable<ArticleViewModel> result = null;
            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            if (response.Succeeded)
            {
                List<GetAllArticleCachedResponse> dataResult = response.Data.OrderByDescending(x => x.PostedDatetime).ToList();

                if (!string.IsNullOrEmpty(slug))
                {
                    if (slug == "tin-moi")
                    {
                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.IsHot == false).Take(30));
                    }
                    else if (slug == "tin-nong")
                    {
                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.IsHot == true).Take(30));
                    }
                    else
                    {
                        var groupCategory = await GetIdBySlug(slug);
                        result = _mapper.Map<List<ArticleViewModel>>(dataResult.Where(x => x.GroupCategoryId == groupCategory).Take(30));
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
            var response = await _mediator.Send(new GetArticleCategoryBySlugQuery { Slug = slug });
            if (response.Succeeded)
            {
                if (response.Data != null)
                {
                    var dataResponse = _mapper.Map<NavigationViewModel>(response.Data);
                    result = dataResponse.Id;
                }

            }
            return result;
        }
    }
}
