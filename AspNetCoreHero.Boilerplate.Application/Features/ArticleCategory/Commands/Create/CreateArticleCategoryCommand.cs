using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.ArticleCategory.Commands.Create
{
    public partial class CreateArticleCategoryCommand : IRequest<Result<int>>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
    }

    public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, Result<int>>
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateArticleCategoryCommandHandler(IArticleCategoryRepository articleCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
        {
            var articleCategory = _mapper.Map<AspNetCoreHero.Boilerplate.Domain.Entities.ArticleCategory>(request);
            await _articleCategoryRepository.InsertAsync(articleCategory);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(articleCategory.Id);
        }
    }
}