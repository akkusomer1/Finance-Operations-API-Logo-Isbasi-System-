using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Ayarlar.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.ProductBrands.Commands.Delete;
public class DeleteProductBrandCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

}
public class DeleteProductBrandCommandHandler : IRequestHandler<DeleteProductBrandCommand, Result<int>>
{
    private readonly IProductBrandRepository _ProductBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductBrandCommandHandler(IProductBrandRepository ProductBrandRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ProductBrandRepository = ProductBrandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(DeleteProductBrandCommand request, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.GetByIdAsync(request.Id);
        await _ProductBrandRepository.DeleteAsync(ProductBrand);
        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(ProductBrand.Id, 200);
    }
}
