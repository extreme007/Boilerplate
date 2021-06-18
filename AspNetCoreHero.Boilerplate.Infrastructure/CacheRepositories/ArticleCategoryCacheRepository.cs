using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
  
    public class ArticleCategoryCacheRepository : IArticleCategoryCacheRepository
    {
        private readonly ICacheService _cacheService;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryCacheRepository(ICacheService cacheService, IArticleCategoryRepository articleCategoryRepository)
        {
            _cacheService = cacheService;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public async Task<ArticleCategory> GetByIdAsync(int articleCategoryId)
        {
            string cacheKey = ArticleCategoryCacheKeys.GetKey(articleCategoryId);
            var articleCategory = await _cacheService.GetAsync<ArticleCategory>(cacheKey);
            if (articleCategory == null)
            {
                articleCategory = await _articleCategoryRepository.GetByIdAsync(articleCategoryId);
                Throw.Exception.IfNull(articleCategory, "ArticleCategory", "No ArticleCategory Found");
                await _cacheService.SetAsync(cacheKey, articleCategory);
            }
            return articleCategory;
        }

        public async Task<List<ArticleCategory>> GetCachedListAsync()
        {
            string cacheKey = ArticleCategoryCacheKeys.ListKey;
            var articleCategoryList = await _cacheService.GetAsync<List<ArticleCategory>>(cacheKey);
            if (articleCategoryList == null)
            {
                articleCategoryList = await _articleCategoryRepository.GetListAsync();
                await _cacheService.SetAsync(cacheKey, articleCategoryList);
            }
            return articleCategoryList;
        }
    }
}