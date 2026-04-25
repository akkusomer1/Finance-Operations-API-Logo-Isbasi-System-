using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.LoanCategories.Queries.GetAllPaged;
public class GetAllLoanCategoriesQuery : IRequest<Result<List<GetAllLoanCategoriesResponse>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<LoanCategory, bool>> Filter { get; set; }
    public GetAllLoanCategoriesQuery(int pageNumber, int pageSize, Expression<Func<LoanCategory, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}
public class GetAllLoanCategoriesQueryHandler : IRequestHandler<GetAllLoanCategoriesQuery, Result<List<GetAllLoanCategoriesResponse>>>
{
    private readonly ILoanCategoryRepository _loanCategoryRepository;

    public GetAllLoanCategoriesQueryHandler(ILoanCategoryRepository loanCategoryRepository)
    {
        _loanCategoryRepository = loanCategoryRepository;
    }
    public async Task<Result<List<GetAllLoanCategoriesResponse>>> Handle(GetAllLoanCategoriesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<LoanCategory, GetAllLoanCategoriesResponse>> expression = e => new GetAllLoanCategoriesResponse
        {
            Id = e.Id,
            Name = e.Name,
        };

        var loanCategory = await _loanCategoryRepository.LoanCategories.Select(expression).ToListAsync();
        return Result<List<GetAllLoanCategoriesResponse>>.Success(loanCategory, 200);
    }
}
