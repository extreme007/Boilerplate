using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IArticleCacheRepository
    {
        Task<List<Article>> GetCachedListAsync(string includeProperties = "");

        Task<Article> GetByIdAsync(int articleId);
        Task<List<Article>> GetByGroupCategoryIdAsync(int groupCategoryId, string includeProperties = "");
    }
}