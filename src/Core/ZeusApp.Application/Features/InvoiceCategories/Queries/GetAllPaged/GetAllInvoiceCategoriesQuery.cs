using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.InvoiceCategories.Queries.GetAllPaged;

public class GetAllInvoiceCategoriesQuery : IRequest<Result<List<GetAllInvoiceCategoriesResponse>>>
{
}

public class GetAllInvoiceCategoriesQueryHandler : IRequestHandler<GetAllInvoiceCategoriesQuery, Result<List<GetAllInvoiceCategoriesResponse>>>
{
    private readonly IInvoiceCategoryRepository _InvoiceCategoryRepository;
    private readonly IMapper _mapper;


    public GetAllInvoiceCategoriesQueryHandler(IInvoiceCategoryRepository InvoiceCategoriesRepository, IMapper mapper)
    {
        _InvoiceCategoryRepository = InvoiceCategoriesRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllInvoiceCategoriesResponse>>> Handle(GetAllInvoiceCategoriesQuery request, CancellationToken cancellationToken)
    {
        var InvoiceCategoriesRepository = await _InvoiceCategoryRepository.GetListAsync();
        var response = _mapper.Map<List<GetAllInvoiceCategoriesResponse>>(InvoiceCategoriesRepository);
        return Result<List<GetAllInvoiceCategoriesResponse>>.Success(response, 200);
    }
}