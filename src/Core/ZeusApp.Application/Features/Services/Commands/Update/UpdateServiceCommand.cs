using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Services.Commands.Update;
using ZeusApp.Application.Features.Services.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using System;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Commands.Update;
public class UpdateServiceCommand : CreateServiceCommand
{
    public int Id { get; set; }
    public int Status { get; set; }
}

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    public UpdateServiceCommandHandler(IProductServiceRepository serviceRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(command.Id);
    
        if (command.Name == null)
        {
            throw new Exception("Hizmet Adı alanın doldurulması zorunludur.");
        }

        if (service == null || service.ProductServiceType == ProductServiceType.product)
        {
            throw new KeyNotFoundException("Bu isimde bir hizmet bulunamadı");
        }

        var updateService = _mapper.Map<UpdateServiceCommand, ProductService>(command, service);

        decimal vatCalculate = (1 + (Convert.ToDecimal(updateService.VATRate) / 100));

        updateService.SalesUnitPriceExcludingVAT = updateService.SalesPriceIncludingVAT / vatCalculate;
        updateService.PurchasePriceExcludingVAT = updateService.PurchasePriceIncludingVAT / vatCalculate;
        updateService.ProductServiceType = ProductServiceType.service;

        await _serviceRepository.UpdateAsync(updateService);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(updateService.Id, 200);
    }
}