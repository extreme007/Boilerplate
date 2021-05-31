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
  
    public class ArticleCacheRepository : IArticleCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IArticleRepository _articleRepository;

        public ArticleCacheRepository(IDistributedCache distributedCache, IArticleRepository articleRepository)
        {
            _distributedCache = distributedCache;
            _articleRepository = articleRepository;
        }

        public async Task<Article> GetByIdAsync(int articleId)
        {
            string cacheKey = ArticleCacheKeys.GetKey(articleId);
            var article = await _distributedCache.GetAsync<Article>(cacheKey);
            if (article == null)
            {
                article = await _articleRepository.GetByIdAsync(articleId);
                Throw.Exception.IfNull(article, "Article", "No Article Found");
                await _distributedCache.SetAsync(cacheKey, article);
            }
            return article;
        }

        public async Task<List<Article>> GetCachedListAsync()
        {
            string cacheKey = ArticleCacheKeys.ListKey;
            var articleList = await _distributedCache.GetAsync<List<Article>>(cacheKey);
            if (articleList == null)
            {
                articleList = await _articleRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, articleList);
            }
            return articleList;
        }
    }
}