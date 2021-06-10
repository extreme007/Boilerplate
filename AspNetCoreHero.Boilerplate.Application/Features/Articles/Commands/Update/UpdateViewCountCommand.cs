using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Update
{
    public class UpdateViewCountCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class UpdateViewCountCommandHandler : IRequestHandler<UpdateViewCountCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IArticleRepository _articleRepository;

            public UpdateViewCountCommandHandler(IArticleRepository artilceRepository, IUnitOfWork unitOfWork)
            {
                _articleRepository = artilceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateViewCountCommand command, CancellationToken cancellationToken)
            {
                var article = await _articleRepository.GetByIdAsync(command.Id);

                if (article == null)
                {
                    return Result<int>.Fail($"Article Not Found.");
                }
                else
                {
                    article.ViewCount = article.ViewCount + 1;
                    await _articleRepository.UpdateAsync(article);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(article.Id);
                }
            }
        }
    }
}