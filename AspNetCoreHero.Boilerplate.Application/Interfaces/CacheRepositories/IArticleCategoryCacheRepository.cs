using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IArticleCategoryCacheRepository
    {
        Task<List<ArticleCategory>> GetCachedListAsync();

        Task<ArticleCategory> GetByIdAsync(int brandId);
    }
}