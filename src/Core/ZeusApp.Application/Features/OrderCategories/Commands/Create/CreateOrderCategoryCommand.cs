using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.OrderCategories.Commands.Create;
public class CreateOrderCategoryCommand : IRequest<Result<int>>
{
    /// <summary>
    /// Borç/Alacak
    /// </summary>
    [Display(Name = "Kategori")]
    public string Name { get; set; }
}
public class CreateOrderCategoryCommandHandler : IRequestHandler<CreateOrderCategoryCommand, Result<int>>
{
    private readonly IOrderCategoryRepository _orderCategoryRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateOrderCategoryCommandHandler(IOrderCategoryRepository orderCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderCategoryRepository = orderCategoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateOrderCategoryCommand request, CancellationToken cancellationToken)
    {
        var orderCategory = _mapper.Map<OrderCategory>(request);

        await _orderCategoryRepository.InsertAsync(orderCategory);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(orderCategory.Id,200);
    }
}
