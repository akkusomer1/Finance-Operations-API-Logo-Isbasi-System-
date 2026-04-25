using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ExpenseCategories.Queries.GetAllPaged;
public class GetAllExpenseCategoriesQuery : IRequest<Result<List<GetAllExpenseCategoriesResponse>>>
{
}
public class GetAllExpenseCategoriesQueryHandler : IRequestHandler<GetAllExpenseCategoriesQuery, Result<List<GetAllExpenseCategoriesResponse>>>
{
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly IMapper _mapper;

    public GetAllExpenseCategoriesQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
    {
        _expenseCategoryRepository = expenseCategoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllExpenseCategoriesResponse>>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var expenseCategories = await _expenseCategoryRepository.GetListAsync();
        var expenseCategoriesResponses = _mapper.Map<List<GetAllExpenseCategoriesResponse>>(expenseCategories);
        return Result<List<GetAllExpenseCategoriesResponse>>.Success(expenseCategoriesResponses, 200);
    }
}
