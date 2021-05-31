using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IBrandCacheRepository
    {
        Task<List<Brand>> GetCachedListAsync();

        Task<Brand> GetByIdAsync(int brandId);
    }
}