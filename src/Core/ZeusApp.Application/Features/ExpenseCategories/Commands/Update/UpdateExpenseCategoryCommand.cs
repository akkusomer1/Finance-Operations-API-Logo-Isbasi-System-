using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ExpenseCategories.Commands.Update;
public class UpdateExpenseCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateExpenseCategoryCommandHandler : IRequestHandler<UpdateExpenseCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public UpdateExpenseCategoryCommandHandler(IUnitOfWork unitOfWork, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task<Result<int>> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var expenseCategory = await _expenseCategoryRepository.GetByIdAsync(request.Id);
            if (expenseCategory == null)
            {
                throw new KeyNotFoundException();
            }
            expenseCategory.Name = request.Name;
            await _expenseCategoryRepository.UpdateAsync(expenseCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(expenseCategory.Id, 200);
        }
    }
}
