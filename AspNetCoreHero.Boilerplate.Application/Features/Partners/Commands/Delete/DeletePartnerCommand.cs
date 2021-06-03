using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Commands.Delete
{
    public class DeletePartnerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, Result<int>>
        {
            private readonly IPartnerRepository _PartnerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeletePartnerCommandHandler(IPartnerRepository PartnerRepository, IUnitOfWork unitOfWork)
            {
                _PartnerRepository = PartnerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePartnerCommand command, CancellationToken cancellationToken)
            {
                var product = await _PartnerRepository.GetByIdAsync(command.Id);
                await _PartnerRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}