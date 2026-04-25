using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Commands.Delete;
public class DeleteServiceCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}
public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Result<int>>
{
    private readonly IRepositoryAsync<ProductService> _serviceRepositoryAsync;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceCommandHandler(IUnitOfWork unitOfWork, IRepositoryAsync<ProductService> repositoryAsync, IRepositoryAsync<ProductService> ServiceRepositoryAsync)
    {
        _unitOfWork = unitOfWork;
        _serviceRepositoryAsync = ServiceRepositoryAsync;
    }

    public async Task<Result<int>> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _serviceRepositoryAsync.GetByIdAsync(command.Id);

        if (service == null || service.ProductServiceType == ProductServiceType.product)
            throw new KeyNotFoundException("Bu isimde bir hizmet bulunamadı");

        await _serviceRepositoryAsync.DeleteAsync(service);
        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(service.Id, 200);
    }
}
