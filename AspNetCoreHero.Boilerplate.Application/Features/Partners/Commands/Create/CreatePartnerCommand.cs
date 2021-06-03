using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Features.Partners.Commands.Create
{
    public partial class CreatePartnerCommand : IRequest<Result<int>>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
    }

    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Result<int>>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreatePartnerCommandHandler(IPartnerRepository partnerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _partnerRepository = partnerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = _mapper.Map<AspNetCoreHero.Boilerplate.Domain.Entities.Partner>(request);
            await _partnerRepository.InsertAsync(partner);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(partner.Id);
        }
    }
}