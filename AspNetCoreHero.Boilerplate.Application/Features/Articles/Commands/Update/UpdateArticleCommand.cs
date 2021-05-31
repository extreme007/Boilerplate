using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Update
{
    public class UpdateArticleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public bool IsHot { get; set; }
        public bool IsRank1 { get; set; }
        public bool IsPublished { get; set; }

        public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IArticleRepository _articleRepository;

            public UpdateArticleCommandHandler(IArticleRepository artilceRepository, IUnitOfWork unitOfWork)
            {
                _articleRepository = artilceRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
            {
                var article = await _articleRepository.GetByIdAsync(command.Id);

                if (article == null)
                {
                    return Result<int>.Fail($"Article Not Found.");
                }
                else
                {
                    article.Title = command.Title;
                    article.Slug = article.Slug;
                    article.Description = command.Description;
                    article.FullDescription =command.FullDescription;
                    article.Content = command.Content;
                    article.Author = command.Author;
                    article.Tags = command.Tags;
                    article.Type = command.Type;
                    article.IsHot = command.IsHot;
                    article.IsRank1 = command.IsRank1;
                    article.IsPublished = command.IsPublished;
                    await _articleRepository.UpdateAsync(article);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(article.Id);
                }
            }
        }
    }
}