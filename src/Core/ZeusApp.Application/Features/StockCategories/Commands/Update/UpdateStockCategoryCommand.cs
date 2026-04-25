using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.StockCategories.Commands.Update;
public class UpdateStockCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateStockCategoryCommandHandler : IRequestHandler<UpdateStockCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockCategoryRepository _stockCategoryRepository;

        public UpdateStockCategoryCommandHandler(IUnitOfWork unitOfWork, IStockCategoryRepository stockCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _stockCategoryRepository = stockCategoryRepository;
        }

        public async Task<Result<int>> Handle(UpdateStockCategoryCommand request, CancellationToken cancellationToken)
        {
            var stockCategory = await _stockCategoryRepository.GetByIdAsync(request.Id);
            if (stockCategory == null)
            {
                throw new KeyNotFoundException();
            }
            stockCategory.Name = request.Name;
            await _stockCategoryRepository.UpdateAsync(stockCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(stockCategory.Id, 200);
        }
    }
}
