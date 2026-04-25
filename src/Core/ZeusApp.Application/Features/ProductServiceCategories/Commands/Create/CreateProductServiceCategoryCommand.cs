using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.ProductServiceCategories.Commands.Create;


public class CreateProductServiceCategoryCommand : IRequest<Result<int>>
{
    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
    public ProductServiceType ProductServiceType { get; set; }
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductServiceCategoryCommand, Result<int>>
{
    private readonly IProductServiceCategoryRepository _productCategoryRepository;
    private readonly IMapper _mapper;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }


    public CreateProductCategoryCommandHandler(IProductServiceCategoryRepository productCategoriesRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _productCategoryRepository = productCategoriesRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateProductServiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var productcategories = _mapper.Map<ProductServiceCategory>(request);

        await _productCategoryRepository.InsertAsync(productcategories);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(productcategories.Id, 200);
    }
}