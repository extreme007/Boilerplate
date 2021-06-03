using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Commands.Update
{
    public class UpdatePartnerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }


        public class UpdateArticleCommandHandler : IRequestHandler<UpdatePartnerCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPartnerRepository _PartnerRepository;

            public UpdateArticleCommandHandler(IPartnerRepository artilceCategoryRepository, IUnitOfWork unitOfWork)
            {
                _PartnerRepository = artilceCategoryRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdatePartnerCommand command, CancellationToken cancellationToken)
            {
                var Partner = await _PartnerRepository.GetByIdAsync(command.Id);

                if (Partner == null)
                {
                    return Result<int>.Fail($"Partner Not Found.");
                }
                else
                {
                    Partner.Title = command.Title;
                    Partner.Slug = command.Slug;
                    Partner.Image = command.Image;
                    Partner.Link = command.Link;
                    Partner.Order =command.Order;
                    await _PartnerRepository.UpdateAsync(Partner);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(Partner.Id);
                }
            }
        }
    }
}