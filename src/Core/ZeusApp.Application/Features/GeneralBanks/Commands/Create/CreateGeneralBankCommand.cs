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

namespace ZeusApp.Application.Features.GeneralBanks.Commands.Create;
public class CreateGeneralBankCommand : IRequest<Result<int>>
{
    [Required(ErrorMessage ="Bu alan zorunludur")]
    public string BankName { get; set; }

    [Required(ErrorMessage = "Bu alan zorunludur")]
    public string BranchName { get; set; }

    public string BranchCode { get; set; }

    public string AuthorizedUser { get; set; }

    public string number { get; set; }

    public ICollection<BankAccountRequest> BankAccounts { get; set; } = new HashSet<BankAccountRequest>();
}
public class CreateGeneralBankCommandHandler : IRequestHandler<CreateGeneralBankCommand, Result<int>>
{
    private readonly IGeneralBankRepository _generalBankRepository;

    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateGeneralBankCommandHandler(IGeneralBankRepository generalBankRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _generalBankRepository = generalBankRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateGeneralBankCommand request, CancellationToken cancellationToken)
    {
        var genelBank = _mapper.Map<GeneralBank>(request);
        var createdResult = await _generalBankRepository.InsertAsync(genelBank);
        await _unitOfWork.Commit(cancellationToken);
        return  Result<int>.Success(genelBank.Id, 200);
    }
}