using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CarrierCompanies.Commands.Delete;
public class DeleteCarrierCompanyCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

  

}
    public class DeleteCarrierCompanyCommandHandler : IRequestHandler<DeleteCarrierCompanyCommand, Result<int>>
    {
        private readonly ICarrierCompanyRepository _carrierCompanyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarrierCompanyCommandHandler(ICarrierCompanyRepository carrierCompanyRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
        _carrierCompanyRepository = carrierCompanyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteCarrierCompanyCommand request, CancellationToken cancellationToken)
        {
            var carrierCompany = await _carrierCompanyRepository.GetByIdAsync(request.Id);
            await _carrierCompanyRepository.DeleteAsync(carrierCompany);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(carrierCompany.Id, 200);
        }
    }
