using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
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
        private readonly IDistributedCache _distributedCache;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryCacheRepository(IDistributedCache distributedCache, IArticleCategoryRepository articleCategoryRepository)
        {
            _distributedCache = distributedCache;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public async Task<ArticleCategory> GetByIdAsync(int articleCategoryId)
        {
            string cacheKey = ArticleCategoryCacheKeys.GetKey(articleCategoryId);
            var articleCategory = await _distributedCache.GetAsync<ArticleCategory>(cacheKey);
            if (articleCategory == null)
            {
                articleCategory = await _articleCategoryRepository.GetByIdAsync(articleCategoryId);
                Throw.Exception.IfNull(articleCategory, "ArticleCategory", "No ArticleCategory Found");
                await _distributedCache.SetAsync(cacheKey, articleCategory);
            }
            return articleCategory;
        }

        public async Task<List<ArticleCategory>> GetCachedListAsync()
        {
            string cacheKey = ArticleCategoryCacheKeys.ListKey;
            var articleCategoryList = await _distributedCache.GetAsync<List<ArticleCategory>>(cacheKey);
            if (articleCategoryList == null)
            {
                articleCategoryList = await _articleCategoryRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, articleCategoryList);
            }
            return articleCategoryList;
        }
    }
}