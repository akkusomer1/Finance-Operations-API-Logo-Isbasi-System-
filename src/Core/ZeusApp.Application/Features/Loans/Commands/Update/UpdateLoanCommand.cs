using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Loans.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.Loans.Commands.Update;
public class UpdateLoanCommand : IRequest<Result<int>>
{
    /// <summary>
    /// Borç/Alacak
    /// </summary>
    public int Id { get; set; }
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

    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Result<int>>
    {
        private readonly ILoanRepository _loanRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateLoanCommandHandler(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id);

            if (loan == null)
            {
                return await Result<int>.FailAsync(404);
            }

            loan.LoanType=request.LoanType;
            loan.DocumentNumber = request.DocumentNumber;
            loan.CustomerSupplierId = request.CustomerSupplierId;
            loan.LoanDate = request.LoanDate;
            loan.Amount = request.Amount;
            loan.LoanCategoryId = request.LoanCategoryId;
            loan.Description = request.Description;

            await _loanRepository.UpdateAsync(loan);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(loan.Id,200);
        }
    }
}
