using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CarrierCompanies.Commands.Update;

public class UpdateCarrierCompanyCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateCarrierCompanyCommandHandler : IRequestHandler<UpdateCarrierCompanyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarrierCompanyRepository _carrierCompanyRepository;

        public UpdateCarrierCompanyCommandHandler(IUnitOfWork unitOfWork, ICarrierCompanyRepository carrierCompanyRepository)
        {
            _unitOfWork = unitOfWork;
            _carrierCompanyRepository = carrierCompanyRepository;
        }

        public async Task<Result<int>> Handle(UpdateCarrierCompanyCommand command, CancellationToken cancellationToken)
        {
            var carrierCompany = await _carrierCompanyRepository.GetByIdAsync(command.Id);
            if (carrierCompany == null)
            {
                throw new KeyNotFoundException();
            }
            carrierCompany.Name = command.Name;
            await _carrierCompanyRepository.UpdateAsync(carrierCompany);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(carrierCompany.Id, 200);
        }
    }

}