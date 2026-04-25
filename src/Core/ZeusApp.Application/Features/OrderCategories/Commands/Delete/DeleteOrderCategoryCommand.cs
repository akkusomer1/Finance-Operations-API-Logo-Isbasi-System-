using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.OrderCategories.Commands.Delete;
public class DeleteOrderCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteOrderCategoryCommandHandler : IRequestHandler<DeleteOrderCategoryCommand, Result<int>>
    {
        private readonly IOrderCategoryRepository _orderCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCategoryCommandHandler(IOrderCategoryRepository orderCategoryRepository, IUnitOfWork unitOfWork)
        {
            _orderCategoryRepository = orderCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteOrderCategoryCommand request, CancellationToken cancellationToken)
        {
            var orderCategory = await _orderCategoryRepository.GetByIdAsync(request.Id);

            await _orderCategoryRepository.DeleteAsync(orderCategory);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(orderCategory.Id,200);
        }
    }
}
