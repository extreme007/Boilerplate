﻿using AspNetCoreHero.Boilerplate.Application.DTOs.Entities.Catalog;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.CacheRepositories
{
    public interface IBrandCacheRepository
    {
        Task<List<BrandDto>> GetCachedListAsync();
        Task<Brand> GetByIdAsync(int brandId);
    }
}
