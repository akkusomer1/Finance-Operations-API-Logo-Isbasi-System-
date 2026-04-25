using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Commands.Update;
public class UpdateStockCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int Status { get; set; }

    [Display(Name = "Tarih")]
    public DateTime Date { get; set; }


    [Display(Name = "Belge No")]
    //   [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string? DocumentNo { get; set; }

    [Display(Name = "Kategori")]
    public int? StockCategoryId { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Stok Harektet Türü")]
    public MovementType MovementType { get; set; }


    //stok giriş

    [Display(Name = "Döviz")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "Döviz Kuru")]
    public decimal ExchangeRate { get; set; }

    [Display(Name = "Genel Toplam")]
    public decimal GrandTotal { get; set; }


    public List<StoctProductUpdateRequest> ProductStocks { get; set; } = new List<StoctProductUpdateRequest>();

}
public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;
    private readonly IProductStockRepository _productStockRepository;
    private readonly IProductServiceRepository _productRepository;
    public UpdateStockCommandHandler(IStockRepository stockRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductStockRepository productStockRepository, IProductServiceRepository productRepository)
    {
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productStockRepository = productStockRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<int>> Handle(UpdateStockCommand command, CancellationToken cancellationToken)
    {
        if (command.ProductStocks.Count == 0)
        {
            throw new Exception("En az bir ürün girilmelidir.");
        }

        var stock = await _stockRepository.GetByIdAsync(command.Id);

        stock.Currency = command.Currency;
        stock.Date = command.Date;
        stock.Description = command.Description;
        stock.DocumentNo = command.DocumentNo;
        stock.ExchangeRate = command.ExchangeRate;
        stock.Id = command.Id;
        stock.StockCategoryId = command.StockCategoryId;
        stock.MovementType = command.MovementType;

        if (stock.StockType == StockType.stockIn)
        {
            stock.ProductStocks.ToList().ForEach(async x =>
            {
                x.ProductService.TotalStockAmount -= x.StockAmount;

                stock.GrandTotal -= x.TotalAmount;
                await _productStockRepository.DeleteAsync(x);
            });

            command.ProductStocks.ForEach(async p =>
            {
                var product = _productRepository.GetByIdAsync(p.ProductServiceId).Result;

                if (product.ProductServiceType != ProductServiceType.product)
                {
                    throw new Exception($"{product.Name} isminde bir ürün bulunamadı!");
                }


                product!.TotalStockAmount += p.StockAmount;


                decimal totalAmount = p.StockAmount * p.PurchasePriceExcludingVAT;

                stock.GrandTotal += totalAmount;

                stock.ProductStocks.Add(new ProductStock
                {
                    ProductServiceId = p.ProductServiceId,
                    StockAmount = p.StockAmount,
                    TotalAmount = totalAmount,
                    PurchasePriceExcludingVAT = p.PurchasePriceExcludingVAT,
                    UnitId = p.UnitId
                });
                await _productRepository.UpdateAsync(product);
            });
            await _stockRepository.UpdateAsync(stock);

            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(stock.Id, 200);
        }


        stock.ProductStocks.ToList().ForEach(async x =>
        {
            x.ProductService.TotalStockAmount += x.StockAmount;
            await _productStockRepository.DeleteAsync(x);
        });

        command.ProductStocks.ForEach(async p =>
        {
            var product = _productRepository.GetByIdAsync(p.ProductServiceId).Result;
            product!.TotalStockAmount -= p.StockAmount;

            stock.ProductStocks.Add(new ProductStock
            {
                ProductServiceId = p.ProductServiceId,
                StockAmount = p.StockAmount,
                UnitId = p.UnitId
            });
            await _productRepository.UpdateAsync(product);
        });
        await _stockRepository.UpdateAsync(stock);

        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(stock.Id, 200);
    }
}