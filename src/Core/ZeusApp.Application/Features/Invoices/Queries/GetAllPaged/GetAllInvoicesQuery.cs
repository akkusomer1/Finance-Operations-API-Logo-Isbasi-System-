using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Invoices.Queries.GetAllPaged;
public class GetAllInvoicesQuery : IRequest<PaginatedResult<GetAllInvoicesResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<Invoice, bool>> Filter { get; set; }

    public GetAllInvoicesQuery(int pageNumber, int pageSize, Expression<Func<Invoice, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}


public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, PaginatedResult<GetAllInvoicesResponse>>
{
    private readonly IInvoiceRepository _repository;
    public GetAllInvoicesQueryHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllInvoicesResponse>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Invoice, GetAllInvoicesResponse>> expression = e => new GetAllInvoicesResponse
        {
            Id = e.Id,
            DueDate = e.DueDate,
            InvoiceDate = e.InvoiceDate,
            InvoiceNumber = e.InvoiceNumber,
            InvoiceType = e.InvoiceType,
            RemainingAmount = e.RemainingAmount,
            TotalAmount = e.TotalAmount,
            SalesInvoiceType = e.SalesInvoiceType,
            Status = e.Status == EntityStatus.Active ? "Aktif" : "İptal edildi",
            NameOrTitle = e.CustomerSupplier.GeneralType == GeneralType.Individual
            ? $"{e.CustomerSupplier.FirstName} {e.CustomerSupplier.LastName}" : e.CustomerSupplier.Title,
            ExchanceCalculate = e.CurrencyType != CurrencyType.TL ? $"{e.ExchangeRate * e.TotalAmount} {e.CurrencyType.ToString()}" : null,
        };


        PaginatedResult<GetAllInvoicesResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.Invoices
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            paginatedList = await _repository.Invoices
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return paginatedList;
    }
}
