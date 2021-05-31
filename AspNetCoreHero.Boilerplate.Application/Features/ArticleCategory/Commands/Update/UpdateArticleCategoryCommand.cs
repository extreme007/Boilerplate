using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Commands.Update
{
    public class UpdateArticleCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }


        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCategoryCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IArticleCategoryRepository _articleCategoryRepository;

            public UpdateArticleCommandHandler(IArticleCategoryRepository artilceCategoryRepository, IUnitOfWork unitOfWork)
            {
                _articleCategoryRepository = artilceCategoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateArticleCategoryCommand command, CancellationToken cancellationToken)
            {
                var articleCategory = await _articleCategoryRepository.GetByIdAsync(command.Id);

                if (articleCategory == null)
                {
                    return Result<int>.Fail($"ArticleCategory Not Found.");
                }
                else
                {
                    articleCategory.Title = command.Title;
                    articleCategory.Slug = command.Slug;
                    articleCategory.ParentId = command.ParentId;
                    articleCategory.Order =command.Order;
                    await _articleCategoryRepository.UpdateAsync(articleCategory);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(articleCategory.Id);
                }
            }
        }
    }
}