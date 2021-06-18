using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Shared
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task<T> SetAsync<T>(string key, T value);
        Task RemoveAsync(string key);
    }
}
