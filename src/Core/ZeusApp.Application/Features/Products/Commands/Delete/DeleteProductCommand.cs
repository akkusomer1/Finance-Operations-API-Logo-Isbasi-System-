using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Commands.Delete;
public class DeleteProductCommand:IRequest<Result<int>>
{
    public int Id { get; set; }
}
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
{
    private readonly IProductServiceRepository _productRepositoryAsync;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductServiceRepository productRepositoryAsync)
    {
        _unitOfWork = unitOfWork;
        _productRepositoryAsync = productRepositoryAsync;
    }

    public async Task<Result<int>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepositoryAsync.GetByIdAsync(command.Id);

        if (product == null || product.ProductServiceType==ProductServiceType.service)
            throw new KeyNotFoundException("Bu isimde bir ürün bulunamadı");

        await _productRepositoryAsync.DeleteAsync(product);
        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(product.Id, 200);
    }
}