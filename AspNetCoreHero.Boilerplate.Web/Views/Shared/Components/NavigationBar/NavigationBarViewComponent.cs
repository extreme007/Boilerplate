using AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Views.Shared.Components.NavigationBar
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IMapper _mapper;
        public NavigationBarViewComponent(IArticleCategoryRepository articleCategoryRepository, IMapper mapper)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _articleCategoryRepository.GetListAsync();
            var viewModel = _mapper.Map<List<NavigationViewModel>>(response);
            return View(viewModel);
        }
    }
}