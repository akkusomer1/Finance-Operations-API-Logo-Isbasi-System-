using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Products.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Commands.Update;
public class UpdateProductCommand : CreateProductCommand
{
    public int Id { get; set; }

    [Display(Name = "Aktif mi?")]
    public int Status { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _productRepository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IProductServiceRepository ProductRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = ProductRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);
        
        if (command.Name == null)
        {
            throw new Exception("Ürün Adı alanın doldurulması zorunludur.");
        }

        if (product == null || product.ProductServiceType == ProductServiceType.service)
        {
            throw new KeyNotFoundException("Bu isimde bir ürün bulunamadı");
        }


        var updateProduct = _mapper.Map<UpdateProductCommand, ProductService>(command, product);

        decimal vatCalculate = (1 + (Convert.ToDecimal(product.VATRate) / 100));

        updateProduct.SalesUnitPriceExcludingVAT = updateProduct.SalesPriceIncludingVAT / vatCalculate;
        updateProduct.PurchasePriceExcludingVAT = updateProduct.PurchasePriceIncludingVAT / vatCalculate;
        product.ProductServiceType = ProductServiceType.product;

        await _productRepository.UpdateAsync(updateProduct);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(updateProduct.Id,200);
    }
}