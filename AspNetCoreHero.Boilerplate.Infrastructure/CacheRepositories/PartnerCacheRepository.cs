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
  
    public class PartnerCacheRepository : IPartnerCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPartnerRepository _PartnerRepository;

        public PartnerCacheRepository(IDistributedCache distributedCache, IPartnerRepository PartnerRepository)
        {
            _distributedCache = distributedCache;
            _PartnerRepository = PartnerRepository;
        }

        public async Task<Partner> GetByIdAsync(int PartnerId)
        {
            string cacheKey = PartnerCacheKeys.GetKey(PartnerId);
            var Partner = await _distributedCache.GetAsync<Partner>(cacheKey);
            if (Partner == null)
            {
                Partner = await _PartnerRepository.GetByIdAsync(PartnerId);
                Throw.Exception.IfNull(Partner, "Partner", "No Partner Found");
                await _distributedCache.SetAsync(cacheKey, Partner);
            }
            return Partner;
        }

        public async Task<List<Partner>> GetCachedListAsync()
        {
            string cacheKey = PartnerCacheKeys.ListKey;
            var PartnerList = await _distributedCache.GetAsync<List<Partner>>(cacheKey);
            if (PartnerList == null)
            {
                PartnerList = await _PartnerRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, PartnerList);
            }
            return PartnerList;
        }
    }
}