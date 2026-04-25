using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ExpenseCategories.Commands.Delete;
public class DeleteExpenseCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand, Result<int>>
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseCategoryCommandHandler(IExpenseCategoryRepository expenseCategoryRepository, IUnitOfWork unitOfWork)
        {
            _expenseCategoryRepository = expenseCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetByIdAsync(request.Id);
            await _expenseCategoryRepository.DeleteAsync(expenseCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(expenseCategory.Id, 200);
        }
    }

}
