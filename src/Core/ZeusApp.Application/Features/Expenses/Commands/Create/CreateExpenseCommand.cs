using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.CaseOrBank;
using ZeusApp.Application.DTOs.Expense;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Expenses.Commands.Create;
public class CreateExpenseCommand : IRequest<Result<int>>
{

    public int? CustomerSupplierId { get; set; }
    // public CustomerSupplier CustomerSupplier { get; set; }

    public DateTime InvoiceDate { get; set; } // Fiş / Fatura Tarihi

    public string InvoiceNo { get; set; } //  Fiş / Fatura No

    public CurrencyType? Currency { get; set; } //Döviz
    public bool PaymentStatus { get; set; }// Ödeme Durumu 0 ise ödendi 1 ise ödenecek

    public string Description { get; set; }// Açıklama
    public int? ExpenseCategoryId { get; set; }//Gider Kategori
    public decimal GrandTotal { get; set; }// Genel Toplam Tl veya Herhangi Döviz
    public decimal DiscountTotal { get; set; }//  Toplam indirimi
    public DiscountType? DiscountType { get; set; }// İndirim Türü
    public List<ExpenseServiceCreateRequest> ExpenseServices { get; set; } = new List<ExpenseServiceCreateRequest>();
    public CaseOrBankRequest CaseOrBank { get; set; } = new CaseOrBankRequest();// case ve bankada hangisi seçtiyse onun bilgiler idolu gelicek
}
public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result<int>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICaseRepository _caseRepository;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateExpenseCommandHandler(
        IExpenseRepository expenseRepository, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IBankAccountRepository bankAccountRepository,
        ICaseRepository caseRepository)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _bankAccountRepository = bankAccountRepository;
        _caseRepository = caseRepository;
    }

    public async Task<Result<int>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        if (request.ExpenseServices.Count == 0)
        {
            throw new Exception("En az bir ürün girilmelidir.");
        }

        if (request.PaymentStatus == true)//ödendi durumları
        {
            if (request.CaseOrBank.Cases != null) // kasa Null değilse kasadan ödedi
            {
                var @casa = await _caseRepository.GetByIdAsync(request.CaseOrBank.Cases.Id);
                casa.OpeningBalance -= request.GrandTotal;
                _caseRepository.UpdateAsync(casa);
            }
            if (request.CaseOrBank.BankAccount != null) //bankalarda acountu seçmiş oluyor
            {
                var bankAccount = await _bankAccountRepository.GetByIdAsync(request.CaseOrBank.BankAccount.Id);
                bankAccount.Balance -= request.GrandTotal;
                _bankAccountRepository.UpdateAsync(bankAccount);
            }
            //hiç birşey seçmemişse mecbur seçmesi lazım hata döncek

            throw new KeyNotFoundException();
        }
        else //request.PaymentStatus==False   Ödenecek olarak
        {

        }

        return Result<int>.Success(200);
    }
}