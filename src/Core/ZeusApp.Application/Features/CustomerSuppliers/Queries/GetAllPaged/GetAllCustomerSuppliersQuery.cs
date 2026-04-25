using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetAllPaged;
public class GetAllCustomerSuppliersQuery : IRequest<PaginatedResult<GetAllCustomerSuppliersResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<CustomerSupplier, bool>> Filter { get; set; }

    public GetAllCustomerSuppliersQuery(int pageNumber, int pageSize, Expression<Func<CustomerSupplier, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}

public class GetAllCustomerSuppliersQueryHandler : IRequestHandler<GetAllCustomerSuppliersQuery, PaginatedResult<GetAllCustomerSuppliersResponse>>
{
    private readonly ICustomerSupplierRepository _repository;

    public GetAllCustomerSuppliersQueryHandler(ICustomerSupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllCustomerSuppliersResponse>> Handle(GetAllCustomerSuppliersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<CustomerSupplier, GetAllCustomerSuppliersResponse>> expression = e => new GetAllCustomerSuppliersResponse
        {
            Id = e.Id,
            NameOrTitle = e.GeneralType == GeneralType.Individual ? $"{e.FirstName} {e.LastName}" : e.Title,
            City= e.Contact.City,
            CustomerGeneralType= e.GeneralType == GeneralType.Individual ? "Bireysel Müşteri" : "Kurumsal Müşteri",
            CustomerSupplierCode= e.CustomerSupplierCode,
            Status= e.Status == AspNetCoreHero.Abstractions.Enums.EntityStatus.Active ? "Aktif" :"Pasif",
            OpeningBalance= e.OpeningBalance,
            PhoneNumber1 = e.Contact.PhoneNumber1   
        };

        PaginatedResult<GetAllCustomerSuppliersResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.CustomerSuppliers
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            paginatedList = await _repository.CustomerSuppliers
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return paginatedList;
    }
}

