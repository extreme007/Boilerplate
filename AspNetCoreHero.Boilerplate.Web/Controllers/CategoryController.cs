﻿using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetBySlug;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllPaged;
using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetByCategoryId;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Controllers
{
    [AllowAnonymous]
    public class CategoryController : BaseController<CategoryController>
    {
        [Route("{slug}", Name = "category")]
        public async Task<IActionResult> Index(string slug,int? page)
        {
            if (string.IsNullOrEmpty(slug))
            {
                ViewData["Category"] = null;
                return RedirectToAction("Index", "Home");
            }
            int pageNumberRequest = page ?? 1;
            int pageSizeRequest =  _configuration.GetValue<int>("PageSize");
            var category = await GetIdBySlug(slug);

            ArticlePagingViewModel result = new ArticlePagingViewModel();
            if(category != null)
            {
                var groupCategoryId = category.ParentId == null ? category.Id : category.ParentId;
                var response = await _mediator.Send(new GetArticleByCategoryIdQuery(pageNumberRequest, pageSizeRequest, groupCategoryId, category.Id));
                if (response.Succeeded)
                {
                    result.Page = response.Page;
                    result.TotalPages = response.TotalPages;
                    result.TotalCount = response.TotalCount;
                    result.Data = _mapper.Map<List<ArticleViewModel>>(response.Data);
                    result.CategoryId = category.Id;
                    ViewData["Category"] = category;
                }
                return View(result);
            }
            Response.StatusCode = 404;
            return RedirectToAction("Index", "Error");
        }

        [Route("LoadMore/{slug}")]
        public async Task<ActionResult> GetDataArticle(string slug,int? page)
        {
            Thread.Sleep(500);
            int pageNumberRequest = page ?? 1;
            int pageSizeRequest = _configuration.GetValue<int>("PageSize");
            var category = await GetIdBySlug(slug);

            List<ArticleViewModel> result = new List<ArticleViewModel>();
            if(category != null)
            {
                var groupCategoryId = category.ParentId == null? category.Id : category.ParentId;
                var response = await _mediator.Send(new GetArticleByCategoryIdQuery(pageNumberRequest, pageSizeRequest, groupCategoryId, category.Id));
                if (response.Succeeded)
                {
                    result = _mapper.Map<List<ArticleViewModel>>(response.Data);
                }
            }

            return PartialView("_Partial_ArticleItem", result);
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

        private async Task<NavigationViewModel> GetIdBySlug(string slug)
        {
            NavigationViewModel result = null;
            var response = await _mediator.Send(new GetArticleCategoryBySlugQuery { Slug = slug });
            if (response.Succeeded)
            {
                if (response.Data != null)
                {
                    result = _mapper.Map<NavigationViewModel>(response.Data);
                }

            }
            return result;
        }
    }
}
