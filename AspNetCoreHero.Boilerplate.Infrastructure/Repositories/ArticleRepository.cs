using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IRepositoryAsync<Article> _repository;
        private readonly IDistributedCache _distributedCache;

        public ArticleRepository(IDistributedCache distributedCache, IRepositoryAsync<Article> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Article> Article => _repository.Entities;

        public async Task DeleteAsync(Article article)
        {
            await _repository.DeleteAsync(article);
            await _distributedCache.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ArticleCacheKeys.GetKey(article.Id));
        }

        public async Task<Article> GetByIdAsync(int articleId)
        {
            return await _repository.Entities.Where(p => p.Id == articleId).FirstOrDefaultAsync();
        }

        public async Task<List<Article>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Article article)
        {
            await _repository.AddAsync(article);
            await _distributedCache.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            return article.Id;
        }

        public async Task UpdateAsync(Article article)
        {
            await _repository.UpdateAsync(article);
            await _distributedCache.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ArticleCacheKeys.GetKey(article.Id));
        }
    }
}