using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Loans.Commands.Create;
public class CreateLoanCommand : IRequest<Result<int>>
{
    /// <summary>
    /// Borç/Alacak
    /// </summary>
    [Display(Name = "Giriş/Çıkış")]
    public LoanType LoanType { get; set; }
    [Display(Name = "Makbuz/Belge No")]
    public string DocumentNumber { get; set; }
    [Display(Name = "Müşteri/Tedarikçi Adı")]
    public int CustomerSupplierId { get; set; }
    [Display(Name = "Tarih")]
    public DateTime LoanDate { get; set; }
    [Display(Name = "Tutar")]
    public decimal Amount { get; set; }
    [Display(Name = "Kategori")]
    public int LoanCategoryId { get; set; }
    [Display(Name = "Açıklama")]
    public string Description { get; set; }

}
public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Result<int>>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = _mapper.Map<Loan>(request);

        await _loanRepository.InsertAsync(loan);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(loan.Id,200);
    }
}
