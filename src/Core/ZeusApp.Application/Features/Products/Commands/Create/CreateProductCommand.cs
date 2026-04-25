using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Commands.Create;
public class CreateProductCommand : IRequest<Result<int>>
{

    [Display(Name = "Kodu")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string Code { get; set; } = null!;


    [Display(Name = "Ad")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string Name { get; set; } = null!;


    [Display(Name = "KDV Oranı (%)")]
    public int VATRate { get; set; }


    [Display(Name = "Para Birimi")]
    public CurrencyType CurrencyType { get; set; }


    [Display(Name = "SATIŞ Birim Fiyat (KDV Dahil)")]
    public decimal SalesPriceIncludingVAT { get; set; }


    [Display(Name = "SATIŞ Birim Fiyat (KDV Hariç)")]
    public decimal SalesUnitPriceExcludingVAT { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Dahil)")]
    public decimal PurchasePriceIncludingVAT { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Hariç)")]
    public decimal PurchasePriceExcludingVAT { get; set; }

    /// <summary>
    /// İzleme Yöntemi
    /// </summary>
    public TrackingType TrackingType { get; set; }

    [Display(Name = "Barkod")]
    public string? Barcode { get; set; }

    [Display(Name = "Ürün Adı (2)")]
    public string? ProductName2 { get; set; }

    [Display(Name = "Kategori")]
    public int? ProductCategoryId { get; set; }


    [Display(Name = "Marka")]
    public int? ProductBrandId { get; set; }


    [Display(Name = "Birimi")]
  
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public int UnitId { get; set; }
}


public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IProductServiceRepository _productRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateProductCommandHandler(IProductServiceRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        if (request.Name == null)
        {
            throw new Exception("Ürün Adı alanın doldurulması zorunludur.");
        }
        var product = _mapper.Map<ProductService>(request);


        //(1 + (Vergi oranı) 
        decimal vatCalculate = (1 + (Convert.ToDecimal(product.VATRate) / 100));

        product.SalesUnitPriceExcludingVAT = product.SalesPriceIncludingVAT / vatCalculate;
        product.PurchasePriceExcludingVAT = product.PurchasePriceIncludingVAT / vatCalculate;
      
        product.ProductServiceType = ProductServiceType.product;


        await _productRepository.InsertAsync(product);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(product.Id, 200);
    }
}
