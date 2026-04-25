using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ExpenseCategories.Queries.GetById;
public class GetExpenseCategoryByIdQuery : IRequest<Result<GetExpenseCategoryByIdResponse>>
{
    public int Id { get; set; }

    public class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQuery, Result<GetExpenseCategoryByIdResponse>>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IMapper _mapper;

        public GetExpenseCategoryByIdQueryHandler(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetExpenseCategoryByIdResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetByIdAsync(request.Id);
            var getExpenseCategoryByIdResponse = _mapper.Map<GetExpenseCategoryByIdResponse>(expenseCategory);
            return await Result<GetExpenseCategoryByIdResponse>.SuccessAsync(getExpenseCategoryByIdResponse, 200);
        }
    }

}
