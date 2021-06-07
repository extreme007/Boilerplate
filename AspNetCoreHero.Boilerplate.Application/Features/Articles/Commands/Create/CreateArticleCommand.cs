using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Articles.Commands.Create
{
    public partial class CreateArticleCommand : IRequest<Result<int>>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Aid { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string ThumbImage { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string FullLink { get; set; }
        public string SourceImage { get; set; }
        public string SourceName { get; set; }
        public string SourceLink { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public int GroupCategoryId { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime PostedDatetime { get; set; }
        public bool IsHot { get; set; }
        public bool IsRank1 { get; set; }
        public bool IsPublished { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result<int>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateArticleCommandHandler(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(request);
            await _articleRepository.InsertAsync(article);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(article.Id);
        }
    }
}