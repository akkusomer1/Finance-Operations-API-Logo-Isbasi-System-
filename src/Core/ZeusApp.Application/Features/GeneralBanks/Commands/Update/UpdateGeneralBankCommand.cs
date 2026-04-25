using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.DTOs.BankAccount;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.GeneralBanks.Commands.Update;
public class UpdateGeneralBankCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bu alan zorunludur")]
    public string BankName { get; set; }

    [Required(ErrorMessage = "Bu alan zorunludur")]
    public string BranchName { get; set; }

    public string BranchCode { get; set; }

    public string AuthorizedUser { get; set; }

    public string number { get; set; }

    public ICollection<BankAccountResponse> BankAccounts { get; set; } = new HashSet<BankAccountResponse>();
}
public class UpdateGeneralBankCommandHandler : IRequestHandler<UpdateGeneralBankCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGeneralBankRepository _generalBankRepository;
    private readonly IMapper _mapper;

    public UpdateGeneralBankCommandHandler(IUnitOfWork unitOfWork, IGeneralBankRepository generalBankRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _generalBankRepository = generalBankRepository;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(UpdateGeneralBankCommand request, CancellationToken cancellationToken)
    { 
        var modelCorporate = await _generalBankRepository.GetByIdAsync(request.Id);

        if (modelCorporate == null)
        {
            throw new KeyNotFoundException("Bu isimde bir Banka Bulunamadı");
        }
        var updateCorporateModel = _mapper.Map<UpdateGeneralBankCommand, GeneralBank>(request, modelCorporate);

        await _generalBankRepository.UpdateAsync(updateCorporateModel);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(updateCorporateModel.Id, 200);
    }
}