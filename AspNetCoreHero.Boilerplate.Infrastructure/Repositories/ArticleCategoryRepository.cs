using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly IRepositoryAsync<ArticleCategory> _repository;
        private readonly ICacheService _cacheService;

        public ArticleCategoryRepository(ICacheService cacheService, IRepositoryAsync<ArticleCategory> repository)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        public IQueryable<ArticleCategory> ArticleCategory => _repository.Entities;

        public async Task DeleteAsync(ArticleCategory articleCategory)
        {
            await _repository.DeleteAsync(articleCategory);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCategoryCacheKeys.ListKey);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCategoryCacheKeys.GetKey(articleCategory.Id));
        }

        public async Task<ArticleCategory> GetByIdAsync(int articleCategoryId)
        {
            return await _repository.Entities.Where(p => p.Id == articleCategoryId).FirstOrDefaultAsync();
        }

        public async Task<List<ArticleCategory>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ArticleCategory articleCategory)
        {
            await _repository.AddAsync(articleCategory);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCategoryCacheKeys.ListKey);
            return articleCategory.Id;
        }

        public async Task UpdateAsync(ArticleCategory articleCategory)
        {
            await _repository.UpdateAsync(articleCategory);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCategoryCacheKeys.ListKey);
            await _cacheService.RemoveAsync(CacheKeys.ArticleCategoryCacheKeys.GetKey(articleCategory.Id));
        }
    }
}