using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.DTOs.Contact;
using ZeusApp.Application.DTOs.Mail;
using ZeusApp.Application.DTOs.RelatedPerson;
using ZeusApp.Application.Features.Banks.Commands.Create;
using ZeusApp.Application.Features.Contacts.Commands.Create;
using ZeusApp.Application.Features.OtherAddresses.Commands.Create;
using ZeusApp.Application.Features.RelatedPersons.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CustomerSupplieries.Commands.Create;
public class CreateCustomerSupplierCommand : IRequest<Result<int>>
{

    [Display(Name = "Ünvan")]
    public string? Title { get; set; }

    [Display(Name = "Müşteri / Tedarikçi Kodu:")]
    public string? CustomerSupplierCode { get; set; }

    [Display(Name = "Kısa Ünvan")]
    public string? ShortTitle { get; set; }

    [Display(Name = "Tipi *")]
    public CustomerSupplierType CustomerSupplierType { get; set; }
    public GeneralType GeneralType { get; set; }

    [Display(Name = "Vergi Dairesi")]
    public string? TaxOffice { get; set; }

    [Display(Name = "Açılış Bakiyesi")]
    [Precision(25, 2)]
    public decimal OpeningBalance { get; set; }

    [Display(Name = "Açılış Bakiyesi Tarihi")]
    public DateTime OpeningBalanceDate { get; set; }

    [Display(Name = "Döviz *")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "KDV Uygulanmaz")]
    public bool IsVATExempt { get; set; }

    [Display(Name = "Tevkifat Uygulanmaz")]
    public bool IsWithholdingTaxExempt { get; set; }

    [Display(Name = "Ek Vergi Uygulanmaz")]
    public bool IsAdditionalTaxExempt { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Kategori")]
    public int? CustomerCategoryId { get; set; }


    //İndividual Customer
    [Display(Name = "T.C. Kimlik No")]
    public string? TcIdNumber { get; set; }

    [Display(Name = "Ad *")]
    public string? FirstName { get; set; }

    [Display(Name = "Soyad *")]
    public string? LastName { get; set; }


    //CorporateCustomer
    [Display(Name = "Vergi No")]
    public string? TaxNumber { get; set; }


    public ContactRequest Contact { get; set; }
    public List<RelatedPersonRequest> RelatedPersons { get; set; }
    public List<OtherAddressRequest> OtherAddresses { get; set; }
    public List<BankRequest> Banks { get; set; }
}
public class CreateCustomerSupplierCommandHandler : IRequestHandler<CreateCustomerSupplierCommand, Result<int>>
{

    private readonly ICustomerSupplierRepository _customerSupplierRepository;

    private readonly IMapper _mapper;

    public CreateCustomerSupplierCommandHandler(ICustomerSupplierRepository customerSupplierRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _customerSupplierRepository = customerSupplierRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }



    public async Task<Result<int>> Handle(CreateCustomerSupplierCommand request, CancellationToken cancellationToken)
    {

        if (request.GeneralType == GeneralType.Individual)
        {
            if (request.FirstName == null || request.LastName == null)
            {
                throw new Exception("Ad ve Soyad alanları zorunludur!");
            }

            var individual = _mapper.Map<CustomerSupplier>(request);

            individual.TotalBalance = individual.OpeningBalance;
            await _customerSupplierRepository.InsertAsync(individual);

            await _unitOfWork.Commit(cancellationToken);

            return Result<int>.Success(individual.Id, 200);
        }

        if (request.Title == null || request.TaxOffice == null)
        {
            throw new Exception("Ünvan ve Vergi Dairesi alanları zorunludur!");
        }

        var corporate = _mapper.Map<CustomerSupplier>(request);
        corporate.TotalBalance = corporate.OpeningBalance;
        await _customerSupplierRepository.InsertAsync(corporate);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(corporate.Id, 200);
    }
}