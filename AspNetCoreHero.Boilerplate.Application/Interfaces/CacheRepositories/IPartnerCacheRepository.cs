using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IPartnerCacheRepository
    {
        Task<List<Partner>> GetCachedListAsync();

        Task<Partner> GetByIdAsync(int brandId);
    }
}