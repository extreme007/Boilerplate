using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IArticleCacheRepository
    {
        Task<List<Article>> GetCachedListAsync();

        Task<Article> GetByIdAsync(int brandId);
    }
}