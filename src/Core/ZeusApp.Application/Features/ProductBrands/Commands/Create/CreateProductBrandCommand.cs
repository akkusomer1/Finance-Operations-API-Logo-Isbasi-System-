using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.ProductBrands.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.ProductBrands.Commands.Create;
public class CreateProductBrandCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}
public class CreateProductBrandCommandHandler : IRequestHandler<CreateProductBrandCommand, Result<int>>
{
    private readonly IProductBrandRepository _ProductBrandRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateProductBrandCommandHandler(IProductBrandRepository ProductBrandRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ProductBrandRepository = ProductBrandRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateProductBrandCommand request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<ProductBrand>(request);
        await _ProductBrandRepository.InsertAsync(response);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(response.Id, 200);
    }
}