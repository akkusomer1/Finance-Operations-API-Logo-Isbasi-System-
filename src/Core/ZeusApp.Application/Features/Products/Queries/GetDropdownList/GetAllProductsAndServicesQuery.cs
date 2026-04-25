using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Enums;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Queries.GetDropdownList;
public class GetAllProductsAndServicesQuery : IRequest<Result<List<GetAllProductsAndServicesResponse>>>
{
    public SalesInvoiceType SalesInvoiceType { get; set; }
}

public class GetAllProductsAndServicesHandle : IRequestHandler<GetAllProductsAndServicesQuery, Result<List<GetAllProductsAndServicesResponse>>>
{
    private readonly IProductServiceRepository _productRepository;
    public GetAllProductsAndServicesHandle(IProductServiceRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<List<GetAllProductsAndServicesResponse>>> Handle(GetAllProductsAndServicesQuery request, CancellationToken cancellationToken)
    {
        //ToptanSatisFaturasiKDVHaric ise Birim Fiyat (KDV Hariç) fiyatı gönder
        var listResponse = new List<GetAllProductsAndServicesResponse>();

        if (request.SalesInvoiceType == SalesInvoiceType.YeniAlis)
        {
            var listProducts1 = await _productRepository.ProductServices.Include(x => x.Unit)
              .Select(x => new GetAllProductsAndServicesResponse
              {
                  Id = x.Id,
                  ProductOrServiceName = x.Name,
                  TotalStockAmount = x.TotalStockAmount,
                  UnitPrice = x.PurchasePriceExcludingVAT,
                  UnitName = x.Unit.Name,

              }).ToListAsync();


            return Result<List<GetAllProductsAndServicesResponse>>.Success(listResponse, 200);
        }
        var listProducts = await _productRepository.ProductServices.Include(x => x.Unit)
              .Select(x => new GetAllProductsAndServicesResponse
              {
                  Id = x.Id,
                  ProductOrServiceName = x.Name,
                  TotalStockAmount = x.TotalStockAmount,
                  UnitPrice = request.SalesInvoiceType == SalesInvoiceType.ToptanSatisFaturasiKDVHaric ? x.SalesUnitPriceExcludingVAT
                  : x.SalesPriceIncludingVAT,
                  UnitName = x.Unit.Name
              }).ToListAsync();
        return Result<List<GetAllProductsAndServicesResponse>>.Success(listResponse, 200);

    }

}
