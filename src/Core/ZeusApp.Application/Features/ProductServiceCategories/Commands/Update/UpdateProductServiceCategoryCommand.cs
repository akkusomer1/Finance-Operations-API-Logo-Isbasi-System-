using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.ProductServiceCategories.Commands.Update;
public class UpdateProductServiceCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }

    public ProductServiceType ProductServiceType { get; set; }
}



public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductServiceCategoryCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceCategoryRepository _ProductCategoryRepository;

    public UpdateProductCategoryCommandHandler(IProductServiceCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
    {
        _ProductCategoryRepository = ProductCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateProductServiceCategoryCommand command, CancellationToken cancellationToken)
    {
        var productBrands = await _ProductCategoryRepository.GetByIdAsync(command.Id);

        if (productBrands == null)
        {
            throw new KeyNotFoundException();
        }
        productBrands.Name = command.Name;
        await _ProductCategoryRepository.UpdateAsync(productBrands);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(productBrands.Id, 200);
    }
}
