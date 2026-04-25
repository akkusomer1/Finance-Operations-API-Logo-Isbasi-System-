using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;

public class GetAllAyarlarQuery : IRequest<PaginatedResult<GetAllAyarlarResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<Ayar, bool>> Filter { get; set; }

    public GetAllAyarlarQuery(int pageNumber, int pageSize, Expression<Func<Ayar, bool>> filter = null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filter = filter;
    }
}

public class GetAllAyarlarQueryHandler : IRequestHandler<GetAllAyarlarQuery, PaginatedResult<GetAllAyarlarResponse>>
{
    private readonly IAyarRepository _repository;

    public GetAllAyarlarQueryHandler(IAyarRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<GetAllAyarlarResponse>> Handle(GetAllAyarlarQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Ayar, GetAllAyarlarResponse>> expression = e => new GetAllAyarlarResponse
        {
            Id = e.Id,
            Adi = e.Adi,
            SistemAdi = e.SistemAdi,
            Degeri = e.Degeri,
            Image = e.Image,
            CreatedOn = e.CreatedOn,
            Status = e.Status,
            DisplayOrder = e.DisplayOrder,
        };

        PaginatedResult<GetAllAyarlarResponse> paginatedList;
        if (request.Filter == null)
        {
            paginatedList = await _repository.Ayarlar
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            paginatedList = await _repository.Ayarlar
                .Where(request.Filter)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return paginatedList;
    }
}
