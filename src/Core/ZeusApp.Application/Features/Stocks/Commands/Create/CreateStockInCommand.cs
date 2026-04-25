using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Commands.Create;
public class CreateStockInCommand : IRequest<Result<int>>
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



    [Display(Name = "Döviz")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "Döviz Kuru")]
    public decimal ExchangeRate { get; set; }

    [Display(Name = "Genel Toplam")]
    public decimal GrandTotal { get; set; }

    public List<StockInProductCreateRequest> ProductStocks { get; set; } = new List<StockInProductCreateRequest>();

}

public class CreateStockInCommandHandler : IRequestHandler<CreateStockInCommand, Result<int>>
{
    private readonly IProductServiceRepository _productRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateStockInCommandHandler(IProductServiceRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, IStockRepository stockRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    public async Task<Result<int>> Handle(CreateStockInCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductStocks.Count == 0)
        {
            throw new Exception("En az bir ürün girilmelidir.");
        }

     
        Stock stock = new Stock()
        {
            Currency = request.Currency,
            Description = request.Description!,
            DocumentNo = request.DocumentNo,
            StockCategoryId = request.StockCategoryId,
            ExchangeRate = request.ExchangeRate,
            MovementType = request.MovementType,         
            StockType = StockType.stockIn,
            LastModifiedOn=DateTime.UtcNow.ToLocalTime(),
        };
        //Değişiklik tarihi en başta eklenme tarihi olması gerekiyor.


        //Genel Toplam=sum(totalAmount)
        request.ProductStocks.ForEach(async p =>
        {
            var product = await _productRepository.GetByIdAsync(p.ProductServiceId);

            if (product.ProductServiceType!=ProductServiceType.product)
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

       
        await _stockRepository.InsertAsync(stock);

        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(stock.Id, 200);
    }
}

