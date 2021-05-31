using AspNetCoreHero.Boilerplate.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories
{
    public interface IArticleCategoryRepository
    {
        IQueryable<ArticleCategory> ArticleCategory { get; }

        Task<List<ArticleCategory>> GetListAsync();

        Task<ArticleCategory> GetByIdAsync(int articleCategoryId);

        Task<int> InsertAsync(ArticleCategory articleCategory);

        Task UpdateAsync(ArticleCategory articleCategory);

        Task DeleteAsync(ArticleCategory articleCategory);
    }
}