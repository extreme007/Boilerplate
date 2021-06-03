using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IPartnerRepository
    {
        IQueryable<Partner> Partners { get; }

        Task<List<Partner>> GetListAsync();

        Task<Partner> GetByIdAsync(int partner);

        Task<int> InsertAsync(Partner partner);

        Task UpdateAsync(Partner partner);

        Task DeleteAsync(Partner partner);
    }
}