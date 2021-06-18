using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IRepositoryAsync<Article> _repository;
        private readonly ICacheService _cacheService;

        public ArticleRepository(ICacheService cacheService, IRepositoryAsync<Article> repository)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        public IQueryable<Article> Article => _repository.Entities;

        public async Task DeleteAsync(Article article)
        {
            await _repository.DeleteAsync(article);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCacheKeys.GetKey(article.Id));
        }

        public async Task<Article> GetByIdAsync(int articleId)
        {
            return await _repository.Entities.AsNoTracking().Where(p => p.Id == articleId).FirstOrDefaultAsync();
        }

        public async Task<List<Article>> GetListAsync(string includeProperties = "")
        {
            var queryable = _repository.Entities.AsQueryable();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string IncludeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperties);
                }
            }
            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<int> InsertAsync(Article article)
        {
            await _repository.AddAsync(article);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            return article.Id;
        }

        public async Task UpdateAsync(Article article)
        {
            await _repository.UpdateAsync(article);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCacheKeys.ListKey);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCacheKeys.GetKey(article.Id));
        }
    }
}