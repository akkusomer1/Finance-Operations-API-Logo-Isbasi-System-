using System.Collections.Generic;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
public class GetAllCustomerOrSupplierQuery : IRequest<Result<List<GetAllCustomerOrSupplierResponse>>>
{
    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }
}

public class GetAllCustomerOrSupplierQueryHandle : IRequestHandler<GetAllCustomerOrSupplierQuery, Result<List<GetAllCustomerOrSupplierResponse>>>
{
    private readonly ICustomerSupplierRepository _repository;

    public GetAllCustomerOrSupplierQueryHandle(ICustomerSupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<GetAllCustomerOrSupplierResponse>>> Handle(GetAllCustomerOrSupplierQuery request, CancellationToken cancellationToken)
    {
        if (request.InvoiceType == InvoiceType.selling)
        {
            var list = await _repository.CustomerSuppliers.Where(x => x.CustomerSupplierType == CustomerSupplierType.Customer || x.CustomerSupplierType == CustomerSupplierType.CustomerSupplier)
              .Select(x => new GetAllCustomerOrSupplierResponse
              {
                  Id = x.Id,
                  TotalBalance = x.TotalBalance,
                  FullNameOrTitle = x.GeneralType == GeneralType.Individual ? $"{x.FirstName} {x.LastName}" : $"{x.Title}"
              }).ToListAsync();
            return Result<List<GetAllCustomerOrSupplierResponse>>.Success(list, 200);
        }

        var list2 = await _repository.CustomerSuppliers.Where(x => x.CustomerSupplierType == CustomerSupplierType.Supplier || x.CustomerSupplierType == CustomerSupplierType.CustomerSupplier)
           .Select(x => new GetAllCustomerOrSupplierResponse
           {
               Id = x.Id,
               TotalBalance = x.TotalBalance,
               FullNameOrTitle = x.GeneralType == GeneralType.Individual ? $"{x.FirstName} {x.LastName}" : $"{x.Title}"
           }).ToListAsync();
        return Result<List<GetAllCustomerOrSupplierResponse>>.Success(list2, 200);
    }

}
