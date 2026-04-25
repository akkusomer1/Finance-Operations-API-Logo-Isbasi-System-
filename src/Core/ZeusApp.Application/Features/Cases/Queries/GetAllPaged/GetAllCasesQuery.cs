using System.Linq.Expressions;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Cases.Queries.GetAllPaged;
public class GetAllCasesQuery : IRequest<Result<List<GetAllCasesResponse>>>
{
}
public class GetAllCasesQueryHandler : IRequestHandler<GetAllCasesQuery, Result<List<GetAllCasesResponse>>>
{
    private readonly ICaseRepository _caseRepository;

    public GetAllCasesQueryHandler(ICaseRepository caseRepository )
    {
        _caseRepository = caseRepository;
    }

    public async Task<Result<List<GetAllCasesResponse>>> Handle(GetAllCasesQuery request, CancellationToken cancellationToken)
    {
         Expression<Func<Case, GetAllCasesResponse>> expression = e => new GetAllCasesResponse
        {
            Id = e.Id,
            Name = e.Name,
            Currency = e.Currency,
            OpeningBalance = e.OpeningBalance,
            Status = e.Status == EntityStatus.Active ? "Aktif" : "Aktif değil"
        };
        var cases = await _caseRepository.Cases.Select(expression).ToListAsync();
        return Result<List<GetAllCasesResponse>>.Success(cases, 200);
    }
}
