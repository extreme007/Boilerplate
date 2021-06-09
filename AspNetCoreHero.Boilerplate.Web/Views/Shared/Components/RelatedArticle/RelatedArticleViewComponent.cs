using AspNetCoreHero.Boilerplate.Application.Features.Articles.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Controllers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.RelatedArticle
{
    public class RelatedArticleViewComponent : BaseViewComponent<ViewComponent>
    {
        public async Task<IViewComponentResult> InvokeAsync(ArticleViewModel article)
        {
            var viewModel = new List<ArticleViewModel>();
            var response = await _mediator.Send(new GetAllArticleCachedQuery());
            if (response.Succeeded)
            {
                int pageSize = _configuration.GetValue<int>("PageSize");
                var dataFilter = response.Data.Where(x => x.CategoryId == article.CategoryId && x.Id != article.Id).Take(pageSize);
                viewModel = _mapper.Map<List<ArticleViewModel>>(dataFilter);
            }
            return View(viewModel);
        }
    }
}
