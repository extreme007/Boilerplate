using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Commands.Delete
{
    public class DeleteArticleCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryCommand, Result<int>>
        {
            private readonly IArticleCategoryRepository _articleCategoryRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository, IUnitOfWork unitOfWork)
            {
                _articleCategoryRepository = articleCategoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteArticleCategoryCommand command, CancellationToken cancellationToken)
            {
                var product = await _articleCategoryRepository.GetByIdAsync(command.Id);
                await _articleCategoryRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}