using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Services.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Commands.Create;
public class CreateServiceCommand:IRequest<Result<int>>
{
    /// <summary>
    /// Kodu
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Hizmet Ad
    /// </summary>
    public string Name { get; set; } = null!;


    /// <summary>
    /// KDV Oranı (%)
    /// </summary>
    public int VATRate { get; set; }


    /// <summary>
    /// Para Birimi
    /// </summary>
    public CurrencyType CurrencyType { get; set; }


    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal SalesPriceIncludingVAT { get; set; }

    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal SalesUnitPriceExcludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }


    //Navigation

    /// <summary>
    /// Hizmet Grubu
    /// </summary>
    public int? ServiceGroupId { get; set; }

    /// <summary>
    /// Hizmet kategori
    /// </summary>
    public int? ServiceCategoryId { get; set; }

    /// <summary>
    /// Birimi 
    /// </summary>
    /// 
    public int? UnitId { get; set; }    
}


public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Result<int>>
{
    private readonly IProductServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateServiceCommandHandler(IProductServiceRepository serviceRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        if (request.Name==null)
        {
            throw new Exception("Hizmet Adı, alanın doldurulması zorunludur.");
        }
        var service = _mapper.Map<ProductService>(request);

        decimal vatCalculate = (1 + (Convert.ToDecimal(service.VATRate) / 100));

        service.SalesUnitPriceExcludingVAT = service.SalesPriceIncludingVAT / vatCalculate;
        service.PurchasePriceExcludingVAT = service.PurchasePriceIncludingVAT / vatCalculate;
        service.ProductServiceType = ProductServiceType.service;

        await _serviceRepository.InsertAsync(service);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(service.Id,200);
    }
}
