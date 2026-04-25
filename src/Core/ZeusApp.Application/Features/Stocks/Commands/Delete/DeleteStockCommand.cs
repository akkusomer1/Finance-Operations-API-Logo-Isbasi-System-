using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Stocks.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Commands.Delete;
public class DeleteStockCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}
public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Result<int>>
{
    private readonly IStockRepository _stockRepositoryAsync;
    private readonly IProductStockRepository _productStockRepositoryAsync;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _productRepository;
    public DeleteStockCommandHandler(IUnitOfWork unitOfWork, IStockRepository stockRepositoryAsync, IProductServiceRepository productRepository, IProductStockRepository productStockRepositoryAsync = null)
    {
        _unitOfWork = unitOfWork;
        _stockRepositoryAsync = stockRepositoryAsync;
        _productRepository = productRepository;
        _productStockRepositoryAsync = productStockRepositoryAsync;
    }

    public async Task<Result<int>> Handle(DeleteStockCommand command, CancellationToken cancellationToken)
    {
        var stock = await _stockRepositoryAsync.GetByIdAsync(command.Id);

        if (stock == null)
            throw new KeyNotFoundException("Bu isimde bir stok kaydı bulunamadı");

        if (stock.StockType == StockType.stockIn)
        {

            stock.ProductStocks.ToList().ForEach(x =>
            {
                x.ProductService.TotalStockAmount -= x.StockAmount;
            });        
        }
        else
        {
            stock.ProductStocks.ToList().ForEach(x =>
            {
                x.ProductService.TotalStockAmount += x.StockAmount;
            });
        }
       
        await _stockRepositoryAsync.DeleteAsync(stock);
        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(stock.Id, 200);
    }
}