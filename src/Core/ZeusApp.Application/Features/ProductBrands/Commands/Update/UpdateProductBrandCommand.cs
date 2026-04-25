using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Contacts.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.ProductBrands.Commands.Update;
public class UpdateProductBrandCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
}



public class UpdateProductBrandCommandHandler : IRequestHandler<UpdateProductBrandCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductBrandRepository _ProductBrandRepository;

    public UpdateProductBrandCommandHandler(IProductBrandRepository ProductBrandRepository, IUnitOfWork unitOfWork)
    {
        _ProductBrandRepository = ProductBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateProductBrandCommand command, CancellationToken cancellationToken)
    {
        var productBrands = await _ProductBrandRepository.GetByIdAsync(command.Id);

        if (productBrands == null)
        {
            throw new KeyNotFoundException();
        }
        productBrands.Name = command.Name;
        await _ProductBrandRepository.UpdateAsync(productBrands);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(productBrands.Id, 200);
    }
}
