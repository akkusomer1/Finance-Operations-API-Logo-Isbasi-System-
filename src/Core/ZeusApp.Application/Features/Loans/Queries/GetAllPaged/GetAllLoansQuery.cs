using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Loans.Queries.GetAllPaged;
public class GetAllLoansQuery : IRequest<PaginatedResult<GetAllLoansResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<Loan, bool>> Filter { get; set; }
    public GetAllLoansQuery(int pageNumber, int pageSize, Expression<Func<Loan, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}

public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, PaginatedResult<GetAllLoansResponse>>
{
    private readonly ILoanRepository _loanRepository;

    public GetAllLoansQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<PaginatedResult<GetAllLoansResponse>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
        Expression<Func<Loan, GetAllLoansResponse>> expression = e => new GetAllLoansResponse
        {
            Id = e.Id,
            LoanType = e.LoanType,
            DocumentNumber = e.DocumentNumber,
            CustomerSupplierId = e.CustomerSupplierId,
            Amount = e.Amount,
            LoanCategoryId = e.LoanCategoryId,
            Description = e.Description,
        };

        PaginatedResult<GetAllLoansResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _loanRepository.Loans
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        else
        {
            paginatedList = await _loanRepository.Loans
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        return paginatedList;
    }
}
