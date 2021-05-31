using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Delete
{
    public class DeleteArticleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result<int>>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteArticleCommandHandler(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
            {
                _articleRepository = articleRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
            {
                var product = await _articleRepository.GetByIdAsync(command.Id);
                await _articleRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}