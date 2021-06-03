using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly IRepositoryAsync<Partner> _repository;
        private readonly IDistributedCache _distributedCache;

        public PartnerRepository(IDistributedCache distributedCache, IRepositoryAsync<Partner> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Partner> Partners => _repository.Entities;

        public async Task DeleteAsync(Partner partner)
        {
            await _repository.DeleteAsync(partner);
            await _distributedCache.RemoveAsync(CacheKeys.PartnerCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PartnerCacheKeys.GetKey(partner.Id));
        }

        public async Task<Partner> GetByIdAsync(int partnerId)
        {
            return await _repository.Entities.Where(p => p.Id == partnerId).FirstOrDefaultAsync();
        }

        public async Task<List<Partner>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Partner partner)
        {
            await _repository.AddAsync(partner);
            await _distributedCache.RemoveAsync(CacheKeys.PartnerCacheKeys.ListKey);
            return partner.Id;
        }

        public async Task UpdateAsync(Partner partner)
        {
            await _repository.UpdateAsync(partner);
            await _distributedCache.RemoveAsync(CacheKeys.PartnerCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.PartnerCacheKeys.GetKey(partner.Id));
        }
    }
}