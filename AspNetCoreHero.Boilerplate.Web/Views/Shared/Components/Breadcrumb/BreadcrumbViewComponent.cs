using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Web.Controllers;
using AspNetCoreHero.Boilerplate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.Breadcrumb
{
    //[ViewComponent(Name = "Breadcrumb")]
    public class BreadcrumbViewComponent : BaseViewComponent<ViewComponent>
    {
        public async Task<IViewComponentResult> InvokeAsync(int? parentId,bool isArticle)
        {
            var viewModel = new BreadcrumbViewModel();
            var listCategory = await _mediator.Send(new GetAllArticleCategoryCachedQuery());
            if ( listCategory.Succeeded)
            {
                var data = _mapper.Map<List<NavigationViewModel>>(listCategory.Data);
                var parent = parentId;
                while(parent != null)
                {
                    var parentCategory = data.FirstOrDefault(x => x.Id == parent);
                    viewModel.ListBreadcrumb.Add(parentCategory);
                    parent = parentCategory.ParentId;
                }

                viewModel.ListBreadcrumb.Reverse();
                viewModel.IsArticle = isArticle;
            }
            return View(viewModel);
        }
    }
}
