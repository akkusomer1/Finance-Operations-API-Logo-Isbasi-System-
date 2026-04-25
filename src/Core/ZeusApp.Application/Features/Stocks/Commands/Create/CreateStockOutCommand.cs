using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Commands.Create;
public class CreateStockOutCommand : IRequest<Result<int>>
{
    [Display(Name = "Tarih")]
    public DateTime Date { get; set; }


    [Display(Name = "Belge No")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string DocumentNo { get; set; } = null!;


    [Display(Name = "Kategori")]
    public int? StockCategoryId { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }


    [Display(Name = "Stok Harektet Türü")]
    public MovementType MovementType { get; set; }

    public List<StockOutProductCreateRequest> ProductStocks { get; set; } = new List<StockOutProductCreateRequest>();
}

public class CreateStockOutCommandHandler : IRequestHandler<CreateStockOutCommand, Result<int>>
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    private readonly IProductServiceRepository _productRepository;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateStockOutCommandHandler(IStockRepository stockRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductServiceRepository productRepository)
    {
        _stockRepository = stockRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Result<int>> Handle(CreateStockOutCommand request, CancellationToken cancellationToken)
    {
    
        if (request.ProductStocks.Count == 0)
        {
            throw new Exception("En az bir ürün girilmelidir.");
        }

        var stock = new Stock()
        {          
            Description=request.Description!,
            Date= request.Date, 
            DocumentNo= request.DocumentNo,          
            MovementType= request.MovementType, 
            StockCategoryId= request.StockCategoryId,
            StockType = StockType.stockOut,
            LastModifiedOn = DateTime.UtcNow.ToLocalTime(),
        };
     
        //Değişiklik tarihi en başta eklenme tarihi olması gerekiyor.

        request.ProductStocks.ForEach(async x =>
        {
            var product = _productRepository.GetByIdAsync(x.ProductServiceId).Result;
            if (product.ProductServiceType != ProductServiceType.product)
            {
                throw new Exception($"{product.Name} isminde bir ürün bulunamadı!");
            }

            product.TotalStockAmount -= x.StockAmount;
            stock.ProductStocks.Add(new ProductStock
            {
                ProductServiceId = x.ProductServiceId,
                StockAmount = x.StockAmount,
                UnitId = x.UnitId,
              
            });
            await _productRepository.UpdateAsync(product);
        });

        await _stockRepository.InsertAsync(stock);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(stock.Id, 200);
    }
}
