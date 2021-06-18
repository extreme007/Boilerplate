using AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Boilerplate.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.CacheRepositories
{
  
    public class ArticleCacheRepository : IArticleCacheRepository
    {
        private readonly ICacheService _cacheService;
        private readonly IArticleRepository _articleRepository;

        public ArticleCacheRepository(ICacheService cacheService, IArticleRepository articleRepository)
        {
            _cacheService = cacheService;
            _articleRepository = articleRepository;
        }

        public async Task<List<Article>> GetByGroupCategoryIdAsync(int groupCategoryId,string includeProperties = "")
        {
            string cacheKey = ArticleCacheKeys.ListKeyByGroupCategoryId(groupCategoryId);
            var articleList = await _cacheService.GetAsync<List<Article>>(cacheKey);
            if (articleList == null)
            {
                var listResult = await GetCachedListAsync(includeProperties);
                articleList = listResult.Where(x => x.GroupCategoryId == groupCategoryId).ToList();
                await _cacheService.SetAsync(cacheKey, articleList);
            }
            return articleList;
        }

        public async Task<Article> GetByIdAsync(int articleId)
        {
            string cacheKey = ArticleCacheKeys.GetKey(articleId);
            var article = await _cacheService.GetAsync<Article>(cacheKey);
            if (article == null)
            {
                var listArticles = await GetCachedListAsync("ArticleCategory");
                //article = await _articleRepository.GetByIdAsync(articleId);
                article = listArticles.FirstOrDefault(x => x.Id == articleId);
                Throw.Exception.IfNull(article, "Article", "No Article Found");
                await _cacheService.SetAsync(cacheKey, article);
            }
            return article;
        }

        public async Task<List<Article>> GetCachedListAsync(string includeProperties = "")
        {
            string cacheKey = ArticleCacheKeys.ListKey;
            var articleList = await _cacheService.GetAsync<List<Article>>(cacheKey);
            if (articleList == null)
            {
                articleList = await _articleRepository.GetListAsync(includeProperties);
                await _cacheService.SetAsync(cacheKey, articleList);
            }
            return articleList;
        }
    }
}