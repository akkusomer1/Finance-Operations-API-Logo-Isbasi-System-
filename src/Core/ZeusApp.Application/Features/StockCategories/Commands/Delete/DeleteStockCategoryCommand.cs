using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.StockCategories.Commands.Delete;
public class DeleteStockCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public class DeleteStockCategoryCommandHandler : IRequestHandler<DeleteStockCategoryCommand, Result<int>>
    {
        private readonly IStockCategoryRepository _stockCategoryRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public DeleteStockCategoryCommandHandler(IStockCategoryRepository stockCategoryRepository, IUnitOfWork unitOfWork)
        {
            _stockCategoryRepository = stockCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteStockCategoryCommand request, CancellationToken cancellationToken)
        {
            var stockCategory = await _stockCategoryRepository.GetByIdAsync(request.Id);
            await _stockCategoryRepository.DeleteAsync(stockCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(stockCategory.Id, 200);
        }
    }

}
