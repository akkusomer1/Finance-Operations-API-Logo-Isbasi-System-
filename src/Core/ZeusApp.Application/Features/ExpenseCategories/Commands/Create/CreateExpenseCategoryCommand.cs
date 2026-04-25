using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.ExpenseCategories.Commands.Create;
public class CreateExpenseCategoryCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}
public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, Result<int>>
{
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expenseCategoryRepository = expenseCategoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var individual = _mapper.Map<ExpenseCategory>(request);
        await _expenseCategoryRepository.InsertAsync(individual);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(individual.Id, 200);
    }
}