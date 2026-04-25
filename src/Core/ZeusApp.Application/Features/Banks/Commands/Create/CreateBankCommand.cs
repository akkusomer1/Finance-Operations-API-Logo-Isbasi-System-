using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Banks.Commands.Create;

public class CreateBankCommand : IRequest<Result<int>>
{
    public int CustomerSupplierId { get; set; }

    [Display(Name = "Banka Adı")]
    public string BankName { get; set; }

    [Display(Name = "Şube Adı")]
    public string BranchName { get; set; }

    [Display(Name = "Şube Kodu")]
    public string BranchCode { get; set; }

    [Display(Name = "Hesap No *")]
    public string AccountNumber { get; set; }

    [Display(Name = "Döviz")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "IBAN *")]
    public string IBAN { get; set; }

}

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, Result<int>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateBankCommandHandler(IBankRepository bankRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _bankRepository = bankRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var bank = _mapper.Map<Bank>(request);

        await _bankRepository.InsertAsync(bank);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(bank.Id, 200);
    }
}