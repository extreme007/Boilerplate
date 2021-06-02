using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        IQueryable<Article> Article { get; }

        Task<List<Article>> GetListAsync(string includeProperties = "");

        Task<Article> GetByIdAsync(int articleId);

        Task<int> InsertAsync(Article article);

        Task UpdateAsync(Article article);

        Task DeleteAsync(Article article);
    }
}